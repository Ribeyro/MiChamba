using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;

        public ProyectoController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

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
    }
}
