using Microsoft.AspNetCore.Identity;
using MyChamba.Data.UnitofWork;
using MyChamba.Domain.Models;
using MyChamba.DTOs.Register;
using MyChamba.DTOs.Usuario;
using MyChamba.Infrastructure.Models;
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

    public async Task<UsuarioResponse> RegistrarEmpresaAsync(RegisterEmpresaRequest request)
{
    var usuario = new Usuario
    {
        Email = request.Email,
        Password = _passwordHasher.HashPassword(null, request.Password),
        Tipo = "empresa",
        IdTipoUsuario = 1,
        Estado = true,
        FechaCreacion = DateTime.UtcNow
    };

    await _unitOfWork.Repository<Usuario>().AddAsync(usuario);
    await _unitOfWork.Complete();

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

    await _unitOfWork.Repository<Empresa>().AddAsync(empresa);
    await _unitOfWork.Complete();

    return new UsuarioResponse
    {
        Id = (int)usuario.Id,
        Email = usuario.Email,
        TipoUsuario = "empresa"
    };
}

public async Task<UsuarioResponse> RegistrarEstudianteAsync(RegisterEstudianteRequest request)
{
    var usuario = new Usuario
    {
        Email = request.Email,
        Password = _passwordHasher.HashPassword(null, request.Password),
        Tipo = "estudiante",
        IdTipoUsuario = 2,
        Estado = true,
        FechaCreacion = DateTime.UtcNow
    };

    await _unitOfWork.Repository<Usuario>().AddAsync(usuario);
    await _unitOfWork.Complete();

    var estudiante = new Estudiante
    {
        IdUsuario = usuario.Id,
        Nombre = request.Estudiante.Nombre,
        Apellido = request.Estudiante.Apellido,
        Telefono = request.Estudiante.Telefono,
        IdUniversidad = (uint)request.Estudiante.IdUniversidad,
        IdCarrera = (uint)request.Estudiante.IdCarrera
    };

    await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
    await _unitOfWork.Complete();
    
    // Registrar idiomas si existen
    if (request.Estudiante.Idiomas != null && request.Estudiante.Idiomas.Any())
    {
        foreach (var idioma in request.Estudiante.Idiomas)
        {
            var estudianteIdioma = new EstudianteIdioma
            {
                IdEstudiante = usuario.Id,
                IdIdioma = idioma.IdIdioma,
                Nivel = idioma.Nivel
            };

            await _unitOfWork.Repository<EstudianteIdioma>().AddAsync(estudianteIdioma);
        }

        await _unitOfWork.Complete();
    }

    return new UsuarioResponse
    {
        Id = (int)usuario.Id,
        Email = usuario.Email,
        TipoUsuario = "estudiante"
    };
}

}