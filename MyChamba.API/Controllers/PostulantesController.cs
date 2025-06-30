using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.UseCases.Postulaciones.AceptarPostulante;
using MyChamba.Application.UseCases.Postulaciones.ObtenerPostulantes;

namespace MyChamba.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostulantesController(
    IObtenerPostulantesPorProyectoUseCase obtenerPostulantesUseCase,
    IAceptarPostulanteUseCase aceptarPostulanteUseCase
) : ControllerBase
{
    [HttpGet("proyecto/{idProyecto}/postulantes")]
    public async Task<IActionResult> ObtenerPostulantesPorProyecto(uint idProyecto)
    {
        var postulantes = await obtenerPostulantesUseCase.ExecuteAsync(idProyecto);
        if (postulantes == null || postulantes.Count == 0)
            return NotFound("No se encontraron postulantes para este proyecto.");

        return Ok(postulantes);
    }

    [HttpPost("aceptar/{idSolicitud}")]
    public async Task<IActionResult> AceptarPostulante(uint idSolicitud)
    {
        try
        {
            await aceptarPostulanteUseCase.ExecuteAsync(idSolicitud);
            return Ok("Postulante aceptado y notificaciones enviadas.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}