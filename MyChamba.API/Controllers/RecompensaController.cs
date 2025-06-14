using Microsoft.AspNetCore.Mvc;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Recompensa;
using MyChamba.Models;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecompensaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RecompensaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("asignar")]
    public async Task<IActionResult> AsignarRecompensa([FromBody] RecompensaCreateDto dto)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().FindAsync(e => e.IdUsuario == dto.IdEstudiante);
        var proyecto = await _unitOfWork.Repository<Proyecto>().FindAsync(p => p.Id == dto.IdProyecto);
        
        if (!estudiante.Any() || !proyecto.Any())
            return NotFound("Estudiante o proyecto no encontrado.");

        // Validar que el estudiante tenga una entrega APROBADA en el proyecto
        var entrega = await _unitOfWork.Repository<EntregasProyecto>()
            .FindAsync(e => e.IdEstudiante == dto.IdEstudiante &&
                            e.IdProyecto == dto.IdProyecto &&
                            e.EstadoEvaluacion.ToLower() == "aprobado");

        if (!entrega.Any())
            return BadRequest("El estudiante no tiene una entrega aprobada en este proyecto.");

        // Validar que no se haya asignado ya una recompensa
        var recompensaExistente = (await _unitOfWork.Repository<Recompensa>().GetAllAsync())
            .Any(r => r.IdEstudiante == dto.IdEstudiante && r.IdProyecto == dto.IdProyecto);

        if (recompensaExistente)
            return BadRequest("Ya se ha asignado una recompensa a este estudiante por este proyecto.");

        var recompensa = new Recompensa
        {
            IdEstudiante = dto.IdEstudiante,
            IdProyecto = dto.IdProyecto,
            IdTipoRecompensa = dto.IdTipoRecompensa,
            FechaAsignacion = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Recompensa>().AddAsync(recompensa);
        await _unitOfWork.Complete();

        if (dto.IdTipoRecompensa == 2 && dto.TipoCertificado != null && dto.Archivo != null && dto.FechaEmision.HasValue)
        {
            var certificado = new RecompensasCertificado
            {
                IdRecompensa = recompensa.Id,
                Tipo = dto.TipoCertificado,
                Archivo = dto.Archivo,
                FechaEmision = dto.FechaEmision.Value
            };

            await _unitOfWork.Repository<RecompensasCertificado>().AddAsync(certificado);
        }

        if (dto.IdTipoRecompensa == 1 && dto.Monto.HasValue && dto.MetodoPago != null && dto.Estado != null)
        {
            var economica = new RecompensasEconomica
            {
                IdRecompensa = recompensa.Id,
                Monto = dto.Monto.Value,
                MetodoPago = dto.MetodoPago,
                Estado = dto.Estado,
                Fecha = DateTime.UtcNow
            };

            await _unitOfWork.Repository<RecompensasEconomica>().AddAsync(economica);
        }

        await _unitOfWork.Complete();

        return Ok(new RecompensaDto
        {
            Id = recompensa.Id,
            IdEstudiante = recompensa.IdEstudiante,
            IdProyecto = recompensa.IdProyecto,
            IdTipoRecompensa = recompensa.IdTipoRecompensa,
            FechaAsignacion = recompensa.FechaAsignacion
        });
    }

    [HttpPost("economica")]
    public async Task<IActionResult> AgregarRecompensaEconomica([FromBody] RecompensaEconomicaCreateDto dto)
    {
        // Validar existencia de la recompensa base
        var recompensa = await _unitOfWork.Repository<Recompensa>().FindAsync(r => r.Id == dto.IdRecompensa);
        if (!recompensa.Any())
            return NotFound("Recompensa base no encontrada.");

        var recompensaBase = recompensa.First();

        // Validar que no exista ya una recompensa económica para esta recompensa
        var existente = await _unitOfWork.Repository<RecompensasEconomica>()
            .FindAsync(e => e.IdRecompensa == dto.IdRecompensa);
        if (existente.Any())
            return BadRequest("Ya existe una recompensa económica asociada a esta recompensa.");

        // Validar que la entrega del estudiante al proyecto esté aprobada
        var entregaAprobada = await _unitOfWork.Repository<EntregasProyecto>().FindAsync(e =>
            e.IdEstudiante == recompensaBase.IdEstudiante &&
            e.IdProyecto == recompensaBase.IdProyecto &&
            e.EstadoEvaluacion.ToLower() == "aprobado"
        );
        if (!entregaAprobada.Any())
            return BadRequest("No se puede agregar recompensa económica: la entrega aún no ha sido aprobada.");

        // Validaciones básicas del contenido
        if (dto.Monto <= 0)
            return BadRequest("El monto debe ser mayor a cero.");
        if (string.IsNullOrWhiteSpace(dto.MetodoPago) || string.IsNullOrWhiteSpace(dto.Estado))
            return BadRequest("Método de pago y estado son obligatorios.");

        // Crear y guardar la recompensa económica
        var economica = new RecompensasEconomica
        {
            IdRecompensa = dto.IdRecompensa,
            Monto = dto.Monto,
            MetodoPago = dto.MetodoPago,
            Estado = dto.Estado,
            Fecha = DateTime.UtcNow
        };

        await _unitOfWork.Repository<RecompensasEconomica>().AddAsync(economica);
        await _unitOfWork.Complete();

        return Ok("Recompensa económica agregada correctamente.");
    }
    
    // GET: api/recompensas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecompensaDto>>> GetAll()
    {
        var recompensas = await _unitOfWork.Repository<Recompensa>().GetAllAsync();

        var recompensaDtos = recompensas.Select(r => new RecompensaDto
        {
            Id = r.Id,
            IdEstudiante = r.IdEstudiante,
            IdProyecto = r.IdProyecto,
            IdTipoRecompensa = r.IdTipoRecompensa,
            FechaAsignacion = r.FechaAsignacion
        }).ToList();

        return Ok(recompensaDtos);
    }

    // GET: api/recompensas/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetById(uint id)
    {
        var recompensa = await _unitOfWork.Repository<Recompensa>().FindAsync(r => r.Id == id);
        if (!recompensa.Any())
            return NotFound("Recompensa no encontrada.");

        var entity = recompensa.First();

        // Obtenemos el certificado si existe
        var certificado = await _unitOfWork.Repository<RecompensasCertificado>()
            .FindAsync(c => c.IdRecompensa == entity.Id);
        var certificadoDto = certificado.FirstOrDefault() is RecompensasCertificado c
            ? new
            {
                c.Tipo,
                c.Archivo,
                c.FechaEmision
            }
            : null;

        // Obtenemos la recompensa económica si existe
        var economica = await _unitOfWork.Repository<RecompensasEconomica>()
            .FindAsync(e => e.IdRecompensa == entity.Id);
        var economicaDto = economica.FirstOrDefault() is RecompensasEconomica e
            ? new
            {
                e.Monto,
                e.MetodoPago,
                e.Estado,
                e.Fecha
            }
            : null;

        // Retornamos la respuesta combinada
        return Ok(new
        {
            entity.Id,
            entity.IdEstudiante,
            entity.IdProyecto,
            entity.IdTipoRecompensa,
            entity.FechaAsignacion,
            Certificado = certificadoDto,
            RecompensaEconomica = economicaDto
        });
    }
    
    [HttpPut("economica/{id}")]
    public async Task<IActionResult> ActualizarRecompensaEconomica(uint id, [FromBody] RecompensaEconomicaUpdateDto dto)
    {
        var recompensaEco = await _unitOfWork.Repository<RecompensasEconomica>().FindAsync(r => r.Id == id);
        if (!recompensaEco.Any())
            return NotFound("Recompensa económica no encontrada.");

        var actual = recompensaEco.First();

        // Validar incremento del monto
        if (dto.Monto < actual.Monto)
            return BadRequest("No se permite reducir el monto de la recompensa económica.");

        // Validaciones opcionales
        if (string.IsNullOrWhiteSpace(dto.Estado))
            return BadRequest("El estado no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(dto.MetodoPago))
            return BadRequest("El método de pago no puede estar vacío.");

        // Actualizar campos permitidos
        actual.Monto = dto.Monto;  // Aumentar o mantener
        actual.MetodoPago = dto.MetodoPago;
        actual.Estado = dto.Estado;

        _unitOfWork.Repository<RecompensasEconomica>().Update(actual);
        await _unitOfWork.Complete();

        return Ok("Recompensa económica actualizada correctamente.");
    }

}

