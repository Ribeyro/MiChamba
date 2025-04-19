using Microsoft.AspNetCore.Mvc;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _usuarioService.RegistrarUsuarioAsync(request);
        return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
    }
}