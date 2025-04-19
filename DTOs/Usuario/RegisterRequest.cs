namespace MyChamba.DTOs.Usuario;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int IdTipoUsuario { get; set; } // 1: Empresa, 2: Estudiante
    public EmpresaRequest? Empresa { get; set; }
    public EstudianteRequest? Estudiante { get; set; }
}