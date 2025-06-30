using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.UseCases.Postulaciones.Commands;
using MyChamba.DTOs.Solicitud;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesController (IMediator _mediator) : ControllerBase
{
    [HttpPost("postular")]
    public async Task<IActionResult> Postular([FromBody] CrearSolicitudDto dto)
    {
        var resultado = await _mediator.Send(new PostularEstudianteCommand(dto));
        return Ok(resultado);
    }
}