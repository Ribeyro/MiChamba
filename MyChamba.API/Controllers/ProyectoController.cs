using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Implementations.Commands;
using MyChamba.Services.Implementations.Queries;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectosController(IMediator _mediator) : ControllerBase
{
    [HttpGet("empresa/{idEmpresa}/proyectos")]
    public async Task<IActionResult> GetProyectosPorEmpresa(uint idEmpresa)
    {
        var result = await _mediator.Send(new GetProyectosPorEmpresaQuery(idEmpresa));
        return Ok(result);
    }

    [HttpPost("empresa/proyecto")]
    public async Task<IActionResult> CrearProyecto([FromBody] CrearProyectoDTO dto)
    {
        var result = await _mediator.Send(new CrearProyectoCommand(dto));
        return Ok(result);
    }
}

