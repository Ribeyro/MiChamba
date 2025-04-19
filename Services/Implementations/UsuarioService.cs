using Microsoft.AspNetCore.Identity;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Usuario;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public UsuarioService(IUnitOfWork unitOfWork, IPasswordHasher<Usuario> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioResponse> RegistrarUsuarioAsync(RegisterRequest request)
    {
        // 1. Verificar si el email ya existe
        var usuarioRepo = _unitOfWork.Repository<Usuario>();
        var existe = (await usuarioRepo.FindAsync(u => u.Email == request.Email)).FirstOrDefault();
        if (existe != null)
            throw new Exception("El correo ya está registrado.");

        // 2. Crear usuario base
        var usuario = new Usuario
        {
            Email = request.Email,
            Password = _passwordHasher.HashPassword(null, request.Password),
            Tipo = request.IdTipoUsuario == 1 ? "empresa" : "estudiante",
            IdTipoUsuario = (uint)request.IdTipoUsuario,
            Estado = true,
            FechaCreacion = DateTime.UtcNow
        };

        await usuarioRepo.AddAsync(usuario);
        await _unitOfWork.Complete(); // Para obtener ID generado

        // 3. Crear datos adicionales según tipo
        if (request.IdTipoUsuario == 1 && request.Empresa != null)
        {
            var empresaRepo = _unitOfWork.Repository<Empresa>();
            var empresa = new Empresa
            {
                IdUsuario = usuario.Id,
                Nombre = request.Empresa.Nombre,
                Telefono = request.Empresa.Telefono,
                Direccion = request.Empresa.Direccion,
                Ruc = request.Empresa.Ruc,
                Logo = request.Empresa.Logo,
                IdSector = (uint)request.Empresa.IdSector
            };
            await empresaRepo.AddAsync(empresa);
        }
        else if (request.IdTipoUsuario == 2 && request.Estudiante != null)
        {
            var estudianteRepo = _unitOfWork.Repository<Estudiante>();
            var estudiante = new Estudiante
            {
                IdUsuario = usuario.Id,
                Nombre = request.Estudiante.Nombre,
                Apellido = request.Estudiante.Apellido,
                Telefono = request.Estudiante.Telefono,
                IdCarrera = (uint)request.Estudiante.IdCarrera,
                IdUniversidad = (uint)request.Estudiante.IdUniversidad
            };
            await estudianteRepo.AddAsync(estudiante);
        }

        await _unitOfWork.Complete();

        return new UsuarioResponse
        {
            Id = (int)usuario.Id,
            Email = usuario.Email ?? throw new Exception("El email no puede ser nulo.")
        };
    }
}