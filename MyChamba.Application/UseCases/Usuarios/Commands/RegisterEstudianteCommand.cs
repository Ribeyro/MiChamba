using MediatR;
using MyChamba.DTOs.Register;
using MyChamba.DTOs.Usuario;

namespace MyChamba.Application.UseCases.Usuarios.Commands;

public class RegisterEstudianteCommand :   IRequest<UsuarioResponse>
{
    public RegisterEstudianteRequest Request { get; }

    public RegisterEstudianteCommand(RegisterEstudianteRequest request)
    {
        Request = request;
    }
}