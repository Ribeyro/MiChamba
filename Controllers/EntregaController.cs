using Microsoft.AspNetCore.Mvc;
using MyChamba.Data.UnitofWork;
using MyChamba.DTOs.Proyectos;
using MyChamba.Models;

namespace MyChamba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntregaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EntregaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> PostEntregaProyecto([FromBody] EntregaProyectoCreateDto dto)
    {
        // Validar existencia de estudiante y proyecto
        var estudiante = (await _unitOfWork.Repository<Estudiante>()
            .FindAsync(e => e.IdUsuario == dto.IdEstudiante)).FirstOrDefault();
        var proyecto = (await _unitOfWork.Repository<Proyecto>()
            .FindAsync(p => p.Id == dto.IdProyecto)).FirstOrDefault();

        if (estudiante == null || proyecto == null)
            return NotFound("Estudiante o proyecto no encontrado.");

        // Validar solicitud aprobada
        var solicitud = (await _unitOfWork.Repository<Solicitude>()
                .FindAsync(s => s.IdEstudiante == dto.IdEstudiante && s.IdProyecto == dto.IdProyecto && s.Estado == "Aprobado"))
            .FirstOrDefault();

        if (solicitud == null)
            return BadRequest("El estudiante no ha sido aprobado para este proyecto.");

        // Validar que no haya entregado antes
        var entregaExistente = (await _unitOfWork.Repository<EntregasProyecto>()
                .FindAsync(e => e.IdEstudiante == dto.IdEstudiante && e.IdProyecto == dto.IdProyecto))
            .FirstOrDefault();

        if (entregaExistente != null)
            return Conflict("El estudiante ya ha entregado este proyecto.");

        // Crear entrega
        var entrega = new EntregasProyecto
        {
            IdEstudiante = dto.IdEstudiante,
            IdProyecto = dto.IdProyecto,
            Descripcion = dto.Descripcion,
            Link = dto.Link,
            Fecha = DateTime.UtcNow,
            EstadoEvaluacion = "pendiente",
            Comentarios = "",
            Rendimiento = ""
        };

        await _unitOfWork.Repository<EntregasProyecto>().AddAsync(entrega);
        await _unitOfWork.Complete();

        var response = new
        {
            entrega.Id,
            entrega.IdEstudiante,
            entrega.IdProyecto,
            entrega.Descripcion,
            entrega.Link,
            entrega.Fecha,
            entrega.EstadoEvaluacion
        };

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> EvaluarEntregaProyecto(uint id, [FromBody] EvaluacionEntregaDto dto)
    {
        // Buscar la entrega usando UnitOfWork
        var entrega = (await _unitOfWork.Repository<EntregasProyecto>()
            .FindAsync(e => e.Id == id)).FirstOrDefault();

        if (entrega == null)
            return NotFound("Entrega no encontrada.");

        // Actualizar campos permitidos
        entrega.EstadoEvaluacion = dto.EstadoEvaluacion;
        entrega.Comentarios = dto.Comentarios;
        entrega.Rendimiento = dto.Rendimiento;

        // Guardar cambios
        _unitOfWork.Repository<EntregasProyecto>().Update(entrega);
        await _unitOfWork.Complete();

        return Ok(new
        {
            entrega.Id,
            entrega.IdEstudiante,
            entrega.IdProyecto,
            entrega.EstadoEvaluacion,
            entrega.Comentarios,
            entrega.Rendimiento
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEntregas()
    {
        var entregas = await _unitOfWork.Repository<EntregasProyecto>().GetAllAsync();

        var list = entregas.Select(e => new
        {
            e.Id,
            e.IdEstudiante,
            e.IdProyecto,
            e.Descripcion,
            e.Link,
            e.Fecha,
            e.EstadoEvaluacion
        });

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEntregaPorId(uint id)
    {
        var entrega = (await _unitOfWork.Repository<EntregasProyecto>()
            .FindAsync(e => e.Id == id)).FirstOrDefault();

        if (entrega == null)
            return NotFound("Entrega no encontrada.");

        var response = new
        {
            entrega.Id,
            entrega.IdEstudiante,
            entrega.IdProyecto,
            entrega.Descripcion,
            entrega.Link,
            entrega.Fecha,
            entrega.EstadoEvaluacion
        };

        return Ok(response);
    }
}