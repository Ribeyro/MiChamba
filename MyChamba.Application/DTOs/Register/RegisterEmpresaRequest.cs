using System.ComponentModel.DataAnnotations;
using MyChamba.DTOs.Usuario;

namespace MyChamba.DTOs.Register;

public class RegisterEmpresaRequest
{
    public string Email { get; set; }

    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; }

    public EmpresaRequest Empresa { get; set; }
}