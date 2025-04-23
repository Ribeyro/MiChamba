using MyChamba.DTOs.Register;
using MyChamba.DTOs.Usuario;

namespace MyChamba.Services.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponse> RegistrarEmpresaAsync(RegisterEmpresaRequest request);
    Task<UsuarioResponse> RegistrarEstudianteAsync(RegisterEstudianteRequest request);
}