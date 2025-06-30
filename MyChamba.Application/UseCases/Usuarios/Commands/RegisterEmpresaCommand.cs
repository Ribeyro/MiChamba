using MediatR;
using MyChamba.DTOs.Register;
using MyChamba.DTOs.Usuario;

namespace MyChamba.Application.UseCases.Usuarios.Commands;

public class RegisterEmpresaCommand : IRequest<UsuarioResponse>
{
    public RegisterEmpresaRequest Request { get; }
    public RegisterEmpresaCommand(RegisterEmpresaRequest request)
    {
        Request = request;
    }
}