using Microsoft.AspNetCore.Mvc;
using MyChamba.Services.Implementations.Student;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Controllers.Student
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Obtiene todos los proyectos disponibles con detalles.
        /// </summary>
        [HttpGet("proyectos")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> ObtenerProyectos()
        {
            var proyectos = await _studentService.ObtenerProyectosDisponiblesAsync();
            return Ok(proyectos);
        }

        /// <summary>
        /// Filtra los proyectos por fecha y empresa.
        /// </summary>
        [HttpGet("proyectos/filtrar")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> FiltrarProyectos([FromQuery] string fecha, [FromQuery] uint? empresaId)
        {
            try
            {
                var proyectos = await _studentService.ObtenerProyectosFiltradosAsync(fecha, empresaId);
                return Ok(proyectos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}