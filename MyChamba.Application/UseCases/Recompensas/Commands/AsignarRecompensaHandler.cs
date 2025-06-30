using System.ComponentModel.DataAnnotations;
using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Recompensa;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Recompensas.Commands;

public class AsignarRecompensaHandler : IRequestHandler<AsignarRecompensaCommand, RecompensaDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AsignarRecompensaHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RecompensaDto> Handle(AsignarRecompensaCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var estudiante = await _unitOfWork.Repository<Estudiante>().FindAsync(e => e.IdUsuario == dto.IdEstudiante);
        var proyecto = await _unitOfWork.Repository<Proyecto>().FindAsync(p => p.Id == dto.IdProyecto);
        if (!estudiante.Any() || !proyecto.Any())
            throw new Exception("Estudiante o proyecto no encontrado.");

        var entrega = await _unitOfWork.Repository<EntregasProyecto>()
            .FindAsync(e => e.IdEstudiante == dto.IdEstudiante &&
                            e.IdProyecto == dto.IdProyecto &&
                            e.EstadoEvaluacion.ToLower() == "aprobado");

        if (!entrega.Any())
            throw new Exception("El estudiante no tiene una entrega aprobada en este proyecto.");

        var recompensaExistente = (await _unitOfWork.Repository<Recompensa>().GetAllAsync())
            .Any(r => r.IdEstudiante == dto.IdEstudiante && r.IdProyecto == dto.IdProyecto);

        if (recompensaExistente)
            throw new Exception("Ya se ha asignado una recompensa a este estudiante por este proyecto.");

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

        return new RecompensaDto
        {
            Id = recompensa.Id,
            IdEstudiante = recompensa.IdEstudiante,
            IdProyecto = recompensa.IdProyecto,
            IdTipoRecompensa = recompensa.IdTipoRecompensa,
            FechaAsignacion = recompensa.FechaAsignacion
        };
    }
}