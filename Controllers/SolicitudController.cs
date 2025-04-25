using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Solicitud;
using MyChamba.Services.Interfaces;

namespace MyChamba.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudService _solicitudService;

        public SolicitudesController(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpPost("solicitudes")]
        public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudDto dto)
        {
            try
            {
                var respuesta = await _solicitudService.CrearSolicitudAsync(dto);
                return CreatedAtAction(nameof(CrearSolicitud), new { id = respuesta.Id }, respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}