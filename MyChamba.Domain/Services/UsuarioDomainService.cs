using System.Text.RegularExpressions;
using MyChamba.Domain.Exceptions;

namespace MyChamba.Domain.Services;

public interface IUsuarioDomainService
{
    void ValidarRegistroUsuario(string email, string password, ulong idTipoUsuario, string tipo, List<string> tiposUsuarioExistentes);
}
public class UsuarioDomainService: IUsuarioDomainService
{
    public void ValidarRegistroUsuario(string email, string password, ulong idTipoUsuario, string tipo, List<string> tiposUsuarioExistentes)
    {
        // 1. Validación de email requerido y formato válido
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new RegistroUsuarioException("El correo electrónico no es válido.");

        // 2. Contraseña segura
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new RegistroUsuarioException("La contraseña debe tener al menos 8 caracteres.");

        // 3. Validación de tipo de usuario requerido
        if (string.IsNullOrWhiteSpace(tipo))
            throw new RegistroUsuarioException("Debe especificar un tipo de usuario (empresa o estudiante).");

        // 4. Validación de tipo usuario válido
        if (!tiposUsuarioExistentes.Contains(tipo))
            throw new RegistroUsuarioException("El tipo de usuario no existe.");

        // 5. IdTipoUsuario válido (positivo)
        if (idTipoUsuario == 0)
            throw new RegistroUsuarioException("El ID del tipo de usuario no es válido.");
    }
}