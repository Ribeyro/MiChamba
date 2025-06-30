using MediatR;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Commands;

public class CrearProyectoCommand : IRequest<bool>
{
    public CrearProyectoDTO Dto { get; }

    public CrearProyectoCommand(CrearProyectoDTO dto)
    {
        Dto = dto;
    }
}