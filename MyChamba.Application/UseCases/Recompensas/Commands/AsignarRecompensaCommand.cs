using MediatR;
using MyChamba.DTOs.Recompensa;

namespace MyChamba.Application.UseCases.Recompensas.Commands;

public record AsignarRecompensaCommand(RecompensaCreateDto Dto) : IRequest<RecompensaDto>;
