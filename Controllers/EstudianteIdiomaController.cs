using Microsoft.AspNetCore.Mvc;
using MyChamba.DTOs.Idioma;
using MyChamba.Services.Interfaces;

namespace MyChamba.Controllers
{
    [ApiController]
    [Route("api/estudiante/{estudianteId}/idiomas")]
    public class EstudianteIdiomaController : ControllerBase
    {
        private readonly IEstudianteIdiomaService _estudianteIdiomaService;

        public EstudianteIdiomaController(IEstudianteIdiomaService estudianteIdiomaService)
        {
            _estudianteIdiomaService = estudianteIdiomaService;
        }

        // POST: api/estudiante/{id}/idiomas
        [HttpPost]
        public async Task<IActionResult> AgregarIdiomas(ulong estudianteId, [FromBody] List<IdiomaRequest> idiomas)
        {
            if (idiomas == null || !idiomas.Any())
                return BadRequest("Debe enviar al menos un idioma.");

            await _estudianteIdiomaService.AgregarIdiomasAsync(estudianteId, idiomas);
            return Ok(new { mensaje = "Idiomas agregados correctamente." });
        }

        // GET: api/estudiante/{id}/idiomas
        [HttpGet]
        public async Task<ActionResult<List<IdiomaResponse>>> ObtenerIdiomas(ulong estudianteId)
        {
            var idiomas = await _estudianteIdiomaService.ObtenerIdiomasAsync(estudianteId);
            return Ok(idiomas);
        }
    }
}