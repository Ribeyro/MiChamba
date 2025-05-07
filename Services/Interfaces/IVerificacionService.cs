using MyChamba.Models;

namespace MyChamba.Services.Interfaces;

public interface IVerificacionService
{
    Task EnviarCorreoVerificacionAsync(Usuario usuario);
    Task<bool> VerificarCorreoAsync(string token);
}