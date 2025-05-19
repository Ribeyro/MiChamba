using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Solicitud;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesController : ControllerBase
{
    private readonly ISolicitudService _solicitudService;

    public SolicitudesController(ISolicitudService solicitudService)
    {
        _solicitudService = solicitudService;
    }

    [HttpPost]
    public async Task<IActionResult> Postular([FromBody] CrearSolicitudDto dto)
    {
        try
        {
            var resultado = await _solicitudService.PostularEstudianteAsync(dto);

            if (resultado)
                return Ok(new { mensaje = "Postulación registrada correctamente." });

            return BadRequest(new { mensaje = "No se pudo registrar la postulación." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
