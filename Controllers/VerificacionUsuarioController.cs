using Microsoft.AspNetCore.Mvc;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerificacionUsuarioController(
    IVerificacionService _verificacionService
) : ControllerBase
{
    [HttpGet("verificar")]
    public async Task<IActionResult> VerificarEmail([FromQuery] string token)
    {
        var result = await _verificacionService.VerificarCorreoAsync(token);
        if (!result)
            return BadRequest("Token inválido o expirado.");

        return Ok("Correo verificado correctamente.");
    }
}