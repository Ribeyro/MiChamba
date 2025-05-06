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

    /// <summary>
    /// Obtiene los proyectos publicados por una empresa específica.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProyectoEmpresaDTO>>> GetProyectosPorEmpresa([FromQuery] uint idEmpresa)
    {
        if (idEmpresa == 0)
            return BadRequest("El parámetro 'idEmpresa' es requerido y debe ser mayor a cero.");

        var proyectos = await _proyectoService.ListarPorEmpresaAsync(idEmpresa);

        if (proyectos == null || !proyectos.Any())
            return NotFound($"No se encontraron proyectos para la empresa con ID {idEmpresa}.");

        return Ok(proyectos);
    }

    /// <summary>
    /// Crea un nuevo proyecto para una empresa.
    /// </summary>
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