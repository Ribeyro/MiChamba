using MediatR;
using MyChamba.DTOs.Recompensa;

namespace MyChamba.Application.UseCases.Recompensas.Queries;

public record GetAllRecompensasQuery : IRequest<IEnumerable<RecompensaDto>>;