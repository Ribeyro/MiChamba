using MediatR;
using MyChamba.DTOs.Recompensa;

namespace MyChamba.Application.UseCases.Recompensas.Commands;

public record AgregarRecompensaEconomicaCommand(RecompensaEconomicaCreateDto Dto) : IRequest<string>;