using Microsoft.AspNetCore.Identity;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.UseCases.Auth.Jwt;
using MyChamba.Domain.Models;
using MyChamba.DTOs.Auth;

namespace MyChamba.Application.UseCases.Auth.Login;

public interface ILoginUseCase
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
}

public class LoginUseCase(
    IUsuarioRepository _usuarioRepository,
    IPasswordHasher<Usuario> _passwordHasher,
    IJwtGenerator _jwtGenerator
) : ILoginUseCase
{
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
    
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        if (string.IsNullOrEmpty(usuario.Password))
            throw new Exception("El usuario no tiene contraseña registrada");

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