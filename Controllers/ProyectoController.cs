using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectosController : ControllerBase
{
    private readonly IProyectoService _proyectoService;

    public ProyectosController(IProyectoService proyectoService)
    {
        _proyectoService = proyectoService;
    }

    [HttpPost]
    public async Task<IActionResult> CrearProyecto([FromBody] CrearProyectoDTO dto)
    {
        try
        {
            var resultado = await _proyectoService.CrearProyectoAsync(dto);

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
