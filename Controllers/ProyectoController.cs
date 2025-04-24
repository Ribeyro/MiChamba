using Microsoft.AspNetCore.Mvc;
using MyChamba.Data.UnitofWork;
using MyChamba.Models;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones relacionadas con proyectos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor del controlador de proyectos.
        /// </summary>
        /// <param name="unitOfWork">Instancia del patrón UnitOfWork</param>
        public ProyectosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
                // Validación de formato de fecha dd/MM/yyyy
                if (!DateTime.TryParseExact(dto.FechaLimite, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaLimite))
                {
                    return BadRequest("La fecha debe estar en el formato dd/MM/yyyy.");
                }
                // Validamos que el tipo de recompensa exista
                var tipoRecompensa = await _unitOfWork.Repository<TipoRecompensa>()
                    .GetByIdAsync(dto.IdTipoRecompensa);

                if (tipoRecompensa == null)
                {
                    return NotFound("El tipo de recompensa especificado no existe.");
                }

                // Creamos el proyecto
                var nuevoProyecto = new Proyecto
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    FechaLimite = fechaLimite,
                    Estado = true,
                    NumeroParticipantes = dto.NumeroParticipantes,
                    IdEmpresa = dto.IdEmpresa,
                    IdTipoRecompensa = dto.IdTipoRecompensa
                };

                await _unitOfWork.Repository<Proyecto>().AddAsync(nuevoProyecto);
                await _unitOfWork.Complete();

                return Ok("Proyecto creado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
        

        /// <summary>
        /// Asocia una lista de habilidades a un proyecto existente.
        /// </summary>
        /// <param name="idProyecto">ID del proyecto al que se asociarán las habilidades.</param>
        /// <param name="idHabilidades">Lista de IDs de habilidades a asociar.</param>
        /// <returns>Respuesta HTTP indicando el resultado de la operación.</returns>
        [HttpPost("asociar-habilidades")]
        public async Task<IActionResult> AsociarHabilidades(uint idProyecto, List<uint> idHabilidades)
        {
            if (idHabilidades == null || !idHabilidades.Any())
                return BadRequest("Debe proporcionar al menos una habilidad para asociar.");

            var proyectoRepository = _unitOfWork.Repository<Proyecto>();
            var proyecto = await proyectoRepository.GetByIdAsync((uint)idProyecto);

            if (proyecto == null)
                return NotFound($"Proyecto con ID {idProyecto} no encontrado.");

            var habilidadRepository = _unitOfWork.Repository<Habilidade>();
            var habilidades = await habilidadRepository.FindAsync(h => idHabilidades.Contains(h.Id));

            if (habilidades.Count() != idHabilidades.Count)
                return NotFound("Una o más habilidades no fueron encontradas.");

            foreach (var habilidad in habilidades)
            {
                if (!proyecto.IdHabilidads.Contains(habilidad))
                {
                    proyecto.IdHabilidads.Add(habilidad);
                }
            }

            await _unitOfWork.Complete();
            return Ok($"Habilidades asociadas correctamente al proyecto {proyecto.Nombre}.");
        }
        
        /// <summary>
        /// Obtiene todos los proyectos con su tipo de recompensa y habilidades asociadas.
        /// Puedes filtrar por fecha límite mínima y por empresa.
        /// </summary>
        /// <param name="fechaLimite">Fecha límite mínima (formato: dd/MM/yyyy)</param>
        /// <param name="idEmpresa">ID de la empresa propietaria</param>
        /// <returns>Lista de proyectos que cumplen con los filtros.</returns>
        [HttpGet("completos-filtros-fecha-idEmpresa")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> ObtenerProyectosCompletos(
            [FromQuery] string? fechaLimite,
            [FromQuery] ulong? idEmpresa)
        {
            var proyectos = await _unitOfWork.Repository<Proyecto>()
                .GetAllAsync(includeProperties: "IdTipoRecompensaNavigation,IdHabilidads");

            // Validación y filtrado por fecha (si se provee)
            if (!string.IsNullOrEmpty(fechaLimite) &&
                DateTime.TryParseExact(fechaLimite, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
            {
                proyectos = proyectos.Where(p => p.FechaLimite.Date >= fecha.Date).ToList();
            }

            // Filtrado por empresa (si se provee)
            if (idEmpresa.HasValue)
            {
                proyectos = proyectos.Where(p => p.IdEmpresa == idEmpresa.Value).ToList();
            }

            // Proyección final
            var resultado = proyectos.Select(p => new ProyectoCompletoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaLimite = p.FechaLimite,
                TipoRecompensa = p.IdTipoRecompensaNavigation?.Tipo,
                Habilidades = p.IdHabilidads.Select(h => new HabilidadDto
                {
                    Id = h.Id,
                    Nombre = h.Nombre
                }).ToList()
            }).ToList();

            return Ok(resultado);
        }
        
        /// <summary>
        /// Obtiene todos los proyectos con su tipo de recompensa y habilidades asociadas.
        /// </summary>
        /// <returns>Lista de proyectos con recompensa y habilidades.</returns>
        [HttpGet("completos")]
        public async Task<ActionResult<List<ProyectoCompletoDto>>> ObtenerProyectosCompletos()
        {
            var proyectos = await _unitOfWork.Repository<Proyecto>()
                .GetAllAsync(includeProperties: "IdTipoRecompensaNavigation,IdHabilidads");

            var resultado = proyectos.Select(p => new ProyectoCompletoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaLimite = p.FechaLimite,
                TipoRecompensa = p.IdTipoRecompensaNavigation?.Tipo,
                Habilidades = p.IdHabilidads.Select(h => new HabilidadDto
                {
                    Id = h.Id,
                    Nombre = h.Nombre
                }).ToList()
            }).ToList();

            return Ok(resultado);
        }


    }
}
