namespace MyChamba.DTOs.Usuario;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string TipoUsuario { get; set; } // Ejemplo: "empresa" o "estudiante"
}