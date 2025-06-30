using MediatR;
using MyChamba.DTOs.Recompensa;

namespace MyChamba.Application.UseCases.Recompensas.Commands;

public record ActualizarRecompensaEconomicaCommand(uint Id, RecompensaEconomicaUpdateDto Dto) : IRequest<string>;