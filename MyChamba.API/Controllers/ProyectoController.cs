using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Implementations;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectosController(IListarProyectosPorEmpresaUseCase _listarProyectosPorEmpresaUseCase) : ControllerBase
{
    /// <summary>
    ///     Obtiene los proyectos publicados por una empresa específica.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProyectoEmpresaDTO>>> GetProyectosPorEmpresa([FromQuery] uint idEmpresa)
    {
        if (idEmpresa == 0)
            return BadRequest("El parámetro 'idEmpresa' es requerido y debe ser mayor a cero.");

        var proyectos = await _listarProyectosPorEmpresaUseCase.ListarPorEmpresaAsync(idEmpresa);

        if (proyectos == null || !proyectos.Any())
            return NotFound($"No se encontraron proyectos para la empresa con ID {idEmpresa}.");

        return Ok(proyectos);
    }

    /// <summary>
    ///     Crea un nuevo proyecto para una empresa.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CrearProyecto([FromBody] CrearProyectoDTO dto)
    {
        try
        {
            var resultado = await _listarProyectosPorEmpresaUseCase.CrearProyectoAsync(dto);

            if (resultado)
                return Ok(new { mensaje = "ProyectoDto creado correctamente." });

            return BadRequest(new { mensaje = "No se pudo crear el proyecto." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}