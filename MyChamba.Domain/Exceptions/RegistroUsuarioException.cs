namespace MyChamba.Domain.Exceptions;

public class RegistroUsuarioException : Exception
{
    public RegistroUsuarioException(string message) : base(message) { }
}