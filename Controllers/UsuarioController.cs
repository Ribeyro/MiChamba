using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Register;
using MyChamba.DTOs.Usuario;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("register/empresa")]
    public async Task<IActionResult> RegisterEmpresa([FromBody] RegisterEmpresaRequest request)
    {
        var result = await _usuarioService.RegistrarEmpresaAsync(request);
        return CreatedAtAction(nameof(RegisterEmpresa), new { id = result.Id }, result);
    }

    [HttpPost("register/estudiante")]
    public async Task<IActionResult> RegisterEstudiante([FromBody] RegisterEstudianteRequest request)
    {
        var result = await _usuarioService.RegistrarEstudianteAsync(request);
        return CreatedAtAction(nameof(RegisterEstudiante), new { id = result.Id }, result);
    }
}
