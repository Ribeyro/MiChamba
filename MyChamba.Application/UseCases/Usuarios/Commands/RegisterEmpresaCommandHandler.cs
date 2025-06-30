using MediatR;
using Microsoft.AspNetCore.Identity;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Domain.Models;
using MyChamba.DTOs.Usuario;
using MyChamba.Infrastructure.Models;

namespace MyChamba.Application.UseCases.Usuarios.Commands;

public class RegisterEmpresaCommandHandler (IUnitOfWork _unitOfWork, IPasswordHasher<Usuario> _passwordHasher): IRequestHandler<RegisterEmpresaCommand, UsuarioResponse>
{
    public async Task<UsuarioResponse> Handle(RegisterEmpresaCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

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
}