using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MyChamba.Data;
using MyChamba.Models; // Asegúrate de que este namespace apunte a tu clase Habilidad
using System;
using MiChamba.API.Controllers.DTOs;

namespace MiChamba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabilidadController : ControllerBase
    {
        private readonly MyChambaContext _context;

        public HabilidadController(MyChambaContext context)
        {
            _context = context;
        }

        // GET: api/habilidad
        [HttpGet]
        public async Task<IActionResult> GetHabilidades()
        {
            var habilidades = await _context.Habilidades.ToListAsync();
            return Ok(habilidades);
        }

        // POST: api/habilidad
        [HttpPost]
        public async Task<IActionResult> CrearHabilidad([FromBody] CrearHabilidadDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                return BadRequest("El nombre de la habilidad es requerido.");
            }

            var habilidad = new Habilidade()
            {
                Nombre = dto.Nombre
            };

            _context.Habilidades.Add(habilidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHabilidadPorId), new { id = habilidad.Id }, habilidad);
        }


        // GET: api/habilidad/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabilidadPorId(int id)
        {
            var habilidad = await _context.Habilidades.FindAsync(id);

            if (habilidad == null)
                return NotFound();

            return Ok(habilidad);
        }
    }
}