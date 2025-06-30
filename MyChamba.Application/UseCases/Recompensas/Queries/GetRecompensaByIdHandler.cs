using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Recompensas.Queries;

public class GetRecompensaByIdHandler : IRequestHandler<GetRecompensaByIdQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRecompensaByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(GetRecompensaByIdQuery request, CancellationToken cancellationToken)
    {
        var recompensa = await _unitOfWork.Repository<Recompensa>().FindAsync(r => r.Id == request.Id);
        if (!recompensa.Any())
            throw new Exception("Recompensa no encontrada.");

        var entity = recompensa.First();

        var certificado = await _unitOfWork.Repository<RecompensasCertificado>().FindAsync(c => c.IdRecompensa == entity.Id);
        var economica = await _unitOfWork.Repository<RecompensasEconomica>().FindAsync(e => e.IdRecompensa == entity.Id);

        return new
        {
            entity.Id,
            entity.IdEstudiante,
            entity.IdProyecto,
            entity.IdTipoRecompensa,
            entity.FechaAsignacion,
            Certificado = certificado.FirstOrDefault() is { } c ? new { c.Tipo, c.Archivo, c.FechaEmision } : null,
            RecompensaEconomica = economica.FirstOrDefault() is { } e ? new { e.Monto, e.MetodoPago, e.Estado, e.Fecha } : null
        };
    }
}