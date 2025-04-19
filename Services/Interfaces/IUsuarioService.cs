using MyChamba.DTOs.Usuario;

namespace MyChamba.Services.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponse> RegistrarUsuarioAsync(RegisterRequest request);
}