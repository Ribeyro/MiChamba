using Microsoft.AspNetCore.Mvc;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulantesController : ControllerBase
    {
        private readonly IPostulanteService _postulanteService;

        public PostulantesController(IPostulanteService postulanteService)
        {
            _postulanteService = postulanteService;
        }

        [HttpGet("proyecto/{idProyecto}/postulantes")]
        public async Task<IActionResult> ObtenerPostulantesPorProyecto(uint idProyecto)
        {
            var postulantes = await _postulanteService.ObtenerPostulantesPorProyectoAsync(idProyecto);
            if (postulantes == null || postulantes.Count == 0)
            {
                return NotFound("No se encontraron postulantes para este proyecto.");
            }
            return Ok(postulantes);
        }
        [HttpPost("aceptar/{idSolicitud}")]
        public async Task<IActionResult> AceptarPostulante(uint idSolicitud)
        {
            try
            {
                await _postulanteService.AceptarPostulanteAsync(idSolicitud);
                return Ok("Postulante aceptado y notificaciones enviadas.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}