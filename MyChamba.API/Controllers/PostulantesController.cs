using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.UseCases.Postulaciones.Commands;
using MyChamba.Application.UseCases.Postulaciones.Queries;

namespace MyChamba.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostulantesController(
    IMediator _mediator
) : ControllerBase
{
    [HttpGet("proyecto/{idProyecto}/postulantes")]
    public async Task<IActionResult> ObtenerPostulantesPorProyecto(uint idProyecto)
    {
        var postulantes = await _mediator.Send(new GetPostulantesByProyectoQuery(idProyecto));

        if (postulantes == null || postulantes.Count == 0)
            return NotFound("No se encontraron postulantes para este proyecto.");

        return Ok(postulantes);
    }

    //HttpPost("aceptar/{idSolicitud}")]
    [HttpPut("solicitud/{idSolicitud}/aceptar")]
    public async Task<IActionResult> AceptarPostulante(uint idSolicitud)
    {
        await _mediator.Send(new AceptarPostulanteCommand(idSolicitud));
        return Ok("Postulante aceptado con éxito.");
    }

}