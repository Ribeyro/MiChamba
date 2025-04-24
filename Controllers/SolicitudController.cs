using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Solicitud;
using MyChamba.Models;
using Microsoft.EntityFrameworkCore;

namespace MyChamba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SolicitudController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Crea una nueva solicitud para un estudiante en un proyecto.
        /// </summary>
        /// <param name="dto">Datos de la solicitud.</param>
        /// <returns>Respuesta HTTP con el mensaje de éxito o error.</returns>
        [HttpPost("solicitudes")]
        public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudDto dto)
        {
            try
            {
                // Validar que el estudiante exista
                var estudiante = await _unitOfWork
                    .Repository<Estudiante>()
                    .GetAllAsQueryable()
                    .FirstOrDefaultAsync(e => e.IdUsuario == dto.IdEstudiante);

                if (estudiante == null)
                    return NotFound("El estudiante especificado no existe.");

                // Validar que el proyecto exista
                var proyecto = await _unitOfWork.Repository<Proyecto>()
                    .GetByIdAsync(dto.IdProyecto);

                if (proyecto == null)
                    return NotFound("El proyecto especificado no existe.");

                // Crear la nueva solicitud
                var nuevaSolicitud = new Solicitude
                {
                    IdEstudiante = dto.IdEstudiante,
                    IdProyecto = dto.IdProyecto,
                    ResumenHabilidades = dto.ResumenHabilidades,
                    FechaSolicitud = DateTime.UtcNow,
                    Estado = "Pendiente"
                };

                await _unitOfWork.Repository<Solicitude>().AddAsync(nuevaSolicitud);
                await _unitOfWork.Complete(); // Guarda en DB

                // Mapear a DTO de respuesta
                var respuestaDto = new SolicitudRespuestaDto
                {
                    Id = nuevaSolicitud.Id,
                    IdEstudiante = nuevaSolicitud.IdEstudiante,
                    IdProyecto = nuevaSolicitud.IdProyecto,
                    FechaSolicitud = nuevaSolicitud.FechaSolicitud,
                    ResumenHabilidades = nuevaSolicitud.ResumenHabilidades,
                    Estado = nuevaSolicitud.Estado
                };

                return CreatedAtAction(nameof(CrearSolicitud), new { id = respuestaDto.Id }, respuestaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
