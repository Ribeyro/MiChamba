    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyChamba.Application.DTOs.Student;
    using MyChamba.Application.UseCases.Estudiantes;
    using MyChamba.Application.UseCases.Estudiantes.ObtenerRetosDisponibles;
    using MyChamba.Application.UseCases.Estudiantes.PerfilEstudiante;
    using MyChamba.DTOs.Proyecto;

    namespace MyChamba.Controllers.Student;
    
    [ApiController]
    [Route("api/[controller]")]
    public class StudentControllerGet : ControllerBase
    {
        private readonly IObtenerRetosDisponiblesUseCase _obtenerRetosDisponibles;
        private readonly IEstudianteProfileUseCase _profileUseCase;

        public StudentControllerGet(IObtenerRetosDisponiblesUseCase studentService, IEstudianteProfileUseCase profileService)
        {
            _obtenerRetosDisponibles = studentService;
            _profileUseCase = profileService;
            
        }

        /// <summary>
        ///     Obtiene todos los proyectos disponibles con detalles.
        /// </summary>
        [HttpGet("proyectos")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> ObtenerProyectos()
        {
            var proyectos = await _obtenerRetosDisponibles.ObtenerProyectosDisponiblesAsync();
            return Ok(proyectos);
        }

        /// <summary>
        ///     Filtra los proyectos por fecha y empresa.
        /// </summary>
        [HttpGet("proyectos/filtrar")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> FiltrarProyectos([FromQuery] string fecha,
            [FromQuery] uint? empresaId)
        {
            try
            {
                var proyectos = await _obtenerRetosDisponibles.ObtenerProyectosFiltradosAsync(fecha, empresaId);
                return Ok(proyectos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<EstudianteProfileDto>> GetProfile(ulong idUsuario)
        {
            return await _profileUseCase.GetProfileAsync(idUsuario);
        }

        [HttpPut("{idUsuario}")]
        public async Task<IActionResult> UpdateProfile(ulong idUsuario, [FromBody] UpdateEstudianteProfileDto profileDto)
        {
            await _profileUseCase.UpdateProfileAsync(idUsuario, profileDto);
            return NoContent();
        }


    }