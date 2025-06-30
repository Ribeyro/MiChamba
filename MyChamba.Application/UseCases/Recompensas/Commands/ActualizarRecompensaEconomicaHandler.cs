using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Recompensas.Commands;

public class ActualizarRecompensaEconomicaHandler : IRequestHandler<ActualizarRecompensaEconomicaCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarRecompensaEconomicaHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(ActualizarRecompensaEconomicaCommand request, CancellationToken cancellationToken)
    {
        var recompensaEco = await _unitOfWork.Repository<RecompensasEconomica>().FindAsync(r => r.Id == request.Id);
        if (!recompensaEco.Any())
            throw new Exception("Recompensa económica no encontrada.");

        var actual = recompensaEco.First();

        if (request.Dto.Monto < actual.Monto)
            throw new Exception("No se permite reducir el monto.");

        if (string.IsNullOrWhiteSpace(request.Dto.Estado) || string.IsNullOrWhiteSpace(request.Dto.MetodoPago))
            throw new Exception("Datos inválidos.");

        actual.Monto = request.Dto.Monto;
        actual.MetodoPago = request.Dto.MetodoPago;
        actual.Estado = request.Dto.Estado;

        _unitOfWork.Repository<RecompensasEconomica>().Update(actual);
        await _unitOfWork.Complete();

        return "Recompensa económica actualizada correctamente.";
    }
}