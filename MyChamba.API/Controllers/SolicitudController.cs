using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.UseCases.Postulaciones.PostularEstudiante;
using MyChamba.DTOs.Solicitud;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesController : ControllerBase
{
    private readonly IPostularEstudianteUseCase _solicitudService;

    public SolicitudesController(IPostularEstudianteUseCase solicitudService)
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