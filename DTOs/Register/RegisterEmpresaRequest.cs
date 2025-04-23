using MyChamba.DTOs.Usuario;

namespace MyChamba.DTOs.Register;

public class RegisterEmpresaRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public EmpresaRequest Empresa { get; set; }
}