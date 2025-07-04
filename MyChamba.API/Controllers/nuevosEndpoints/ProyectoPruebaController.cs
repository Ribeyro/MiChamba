using System.Text.Json;
using MiChamba.API.Controllers.DTOs;
using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoPruebaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProyectoPruebaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("crear-proyecto-prueba")]
        public async Task<IActionResult> CrearProyectoPrueba([FromBody] CrearProyectoPruebaDto dto)
        {
            try
            {
                if (dto.IdEmpresa > uint.MaxValue || dto.IdTipoRecompensa > uint.MaxValue)
                    return BadRequest("ID de empresa o recompensa inválido.");

                // Buscar empresa
                var empresa = (await _unitOfWork.Repository<Empresa>()
                    .FindAsync(e => e.IdUsuario == dto.IdEmpresa)).FirstOrDefault();

                if (empresa == null)
                    return BadRequest("La empresa no existe.");

                // Buscar tipo recompensa
                var tipoRecompensa = (await _unitOfWork.Repository<TipoRecompensa>()
                    .FindAsync(r => r.Id == dto.IdTipoRecompensa)).FirstOrDefault();

                if (tipoRecompensa == null)
                    return BadRequest("El tipo de recompensa no existe.");

                // Buscar o crear habilidades
                var habilidadesRepo = _unitOfWork.Repository<Habilidade>();
                var habilidades = new List<Habilidade>();

                foreach (var entrada in dto.IdHabilidades)
                {
                    Habilidade? habilidad = null;

                    if (entrada is JsonElement element)
                    {
                        if (element.ValueKind == JsonValueKind.Number && element.TryGetInt32(out int idInt))
                        {
                            habilidad = (await habilidadesRepo.FindAsync(h => h.Id == (uint)idInt)).FirstOrDefault();
                            if (habilidad == null)
                                return BadRequest($"No se encontró la habilidad con ID {idInt}");
                        }
                        else if (element.ValueKind == JsonValueKind.String)
                        {
                            string nombre = element.GetString()!;
                            habilidad = (await habilidadesRepo.FindAsync(h => h.Nombre.ToLower() == nombre.ToLower()))
                                        .FirstOrDefault();

                            if (habilidad == null)
                            {
                                habilidad = new Habilidade { Nombre = nombre };
                                await habilidadesRepo.AddAsync(habilidad);
                            }
                        }
                    }

                    if (habilidad != null)
                        habilidades.Add(habilidad);
                }

                // Crear proyecto
                var nuevoProyecto = new Proyecto
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    FechaLimite = dto.FechaLimite,
                    Estado = true,
                    NumeroParticipantes = 0,
                    IdEmpresa = dto.IdEmpresa,
                    IdTipoRecompensa = dto.IdTipoRecompensa,
                    IdHabilidads = habilidades
                };

                await _unitOfWork.Repository<Proyecto>().AddAsync(nuevoProyecto);
                await _unitOfWork.Complete();

                return Ok(new
                {
                    mensaje = "✅ Proyecto de prueba creado exitosamente",
                    proyectoId = nuevoProyecto.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "❌ Error al crear el proyecto de prueba",
                    detalle = ex.Message
                });
            }
        }
    }
}
