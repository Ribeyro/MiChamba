using MediatR;
using Microsoft.AspNetCore.Identity;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Domain.Models;
using MyChamba.DTOs.Usuario;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Usuarios.Commands;

public class RegisterEstudianteCommandHandler (IUnitOfWork _unitOfWork, IPasswordHasher<Usuario> _passwordHasher ): IRequestHandler<RegisterEstudianteCommand, UsuarioResponse>
{
    public async Task<UsuarioResponse> Handle(RegisterEstudianteCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

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

        // Registrar idiomas si los hay
        if (request.Estudiante.Idiomas is { Count: > 0 })
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