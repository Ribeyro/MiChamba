namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface ICorreoService
{
    Task EnviarVerificacionEmail(string email, string token);
}