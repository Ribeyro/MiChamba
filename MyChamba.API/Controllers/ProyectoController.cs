using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Implementations;
using MyChamba.Services.Implementations.Commands;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectosController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    ///     Obtiene los proyectos publicados por una empresa específica.
    /// </summary>
    [HttpGet("{idEmpresa}")]
    public async Task<IActionResult> GetProyectos(uint idEmpresa)
    {
        var result = await _mediator.Send(new ListarProyectosPorEmpresaQuery(idEmpresa));
        return Ok(result);
    }


    /// <summary>
    ///     Crea un nuevo proyecto para una empresa.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CrearProyecto([FromBody] CrearProyectoCommand command)
    {
        try
        {
            var resultado = await _mediator.Send(command);

            if (resultado)
                return Ok(new { mensaje = "Proyecto creado correctamente." });

            return BadRequest(new { mensaje = "No se pudo crear el proyecto." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}