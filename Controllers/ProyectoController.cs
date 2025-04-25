using Microsoft.AspNetCore.Mvc;
using MyChamba.Data.Repositories;
using MyChamba.Data.UnitofWork;
using MyChamba.Models;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Implementations;

namespace MyChamba.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones relacionadas con proyectos.
    /// </summary>
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
        /// <summary>
        /// Crea un nuevo proyecto.
        /// </summary>
        /// <param name="dto">Datos necesarios para crear el proyecto.</param>
        /// <returns>Respuesta HTTP con el ID del proyecto creado o mensaje de error.</returns>
        [HttpPost("crear")]
        public async Task<IActionResult> CrearProyecto([FromBody] CrearProyectoDto dto)
        {
            try
            {
                var resultado = await _proyectoService.CrearProyectoAsync(dto);

                if (resultado == "Proyecto creado con éxito.")
                    return Ok(resultado);

                return BadRequest(resultado); // Mensaje de error (ej. fecha o tipo de recompensa no válido)
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
        
        [HttpPost("asociar-habilidades")]
        public async Task<IActionResult> AsociarHabilidades(uint idProyecto, List<uint> idHabilidades)
        {
            var resultado = await _proyectoService.AsociarHabilidadesAsync(idProyecto, idHabilidades);

            if (resultado.StartsWith("Habilidades asociadas"))
                return Ok(resultado);

            return BadRequest(resultado);
        }
        
        [HttpGet("completos")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> ObtenerProyectosCompletos()
        {
            var proyectos = await _proyectoService.ObtenerProyectosCompletosAsync();
            return Ok(proyectos);
        }
        
        [HttpGet("filtrados")]
        public async Task<IActionResult> ObtenerFiltrados([FromQuery] string fecha, [FromQuery] uint? idEmpresa = null)
        {
            var resultado = await _proyectoService.ObtenerProyectosFiltradosAsync(fecha, idEmpresa);
    
            if (resultado == null || !resultado.Any())
                return NotFound("No se encontraron proyectos con los filtros especificados.");

            return Ok(resultado);
        }

        


        
    }
}