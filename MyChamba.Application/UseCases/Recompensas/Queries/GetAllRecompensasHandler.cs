using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Recompensa;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Recompensas.Queries;

public class GetAllRecompensasHandler : IRequestHandler<GetAllRecompensasQuery, IEnumerable<RecompensaDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRecompensasHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RecompensaDto>> Handle(GetAllRecompensasQuery request, CancellationToken cancellationToken)
    {
        var recompensas = await _unitOfWork.Repository<Recompensa>().GetAllAsync();

        return recompensas.Select(r => new RecompensaDto
        {
            Id = r.Id,
            IdEstudiante = r.IdEstudiante,
            IdProyecto = r.IdProyecto,
            IdTipoRecompensa = r.IdTipoRecompensa,
            FechaAsignacion = r.FechaAsignacion
        });
    }
}