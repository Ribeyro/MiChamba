using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Recompensas.Commands;

public class AgregarRecompensaEconomicaHandler : IRequestHandler<AgregarRecompensaEconomicaCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public AgregarRecompensaEconomicaHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(AgregarRecompensaEconomicaCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var recompensa = await _unitOfWork.Repository<Recompensa>().FindAsync(r => r.Id == dto.IdRecompensa);
        if (!recompensa.Any())
            throw new Exception("Recompensa base no encontrada.");

        var recompensaBase = recompensa.First();

        var existente = await _unitOfWork.Repository<RecompensasEconomica>().FindAsync(e => e.IdRecompensa == dto.IdRecompensa);
        if (existente.Any())
            throw new Exception("Ya existe una recompensa económica asociada a esta recompensa.");

        var entregaAprobada = await _unitOfWork.Repository<EntregasProyecto>().FindAsync(e =>
            e.IdEstudiante == recompensaBase.IdEstudiante &&
            e.IdProyecto == recompensaBase.IdProyecto &&
            e.EstadoEvaluacion.ToLower() == "aprobado"
        );
        if (!entregaAprobada.Any())
            throw new Exception("No se puede agregar recompensa económica: la entrega aún no ha sido aprobada.");

        if (dto.Monto <= 0 || string.IsNullOrWhiteSpace(dto.MetodoPago) || string.IsNullOrWhiteSpace(dto.Estado))
            throw new Exception("Datos inválidos para la recompensa económica.");

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

        return "Recompensa económica agregada correctamente.";
    }
}