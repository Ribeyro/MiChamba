using MediatR;

namespace MyChamba.Application.UseCases.Recompensas.Queries;

public record GetRecompensaByIdQuery(uint Id) : IRequest<object>;