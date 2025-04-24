using MyChamba.DTOs.Usuario;

namespace MyChamba.DTOs.Register;

public class RegisterEstudianteRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public EstudianteRequest Estudiante { get; set; }
}