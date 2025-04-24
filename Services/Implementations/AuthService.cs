using Microsoft.AspNetCore.Identity;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Auth;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class AuthService:IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;

    public AuthService(IUsuarioRepository usuarioRepository, IPasswordHasher<Usuario> passwordHasher, IJwtGenerator jwtGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, request.Password);
        if (result != PasswordVerificationResult.Success)
            throw new Exception("Contraseña incorrecta");

        var token = _jwtGenerator.GenerateToken(usuario);

        return new AuthResponse
        {
            Token = token,
            Email = usuario.Email,
            Rol = usuario.IdTipoUsuario.ToString()
        };
    }
}