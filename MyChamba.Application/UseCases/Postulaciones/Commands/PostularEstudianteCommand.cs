using MediatR;
using MyChamba.DTOs.Solicitud;

namespace MyChamba.Application.UseCases.Postulaciones.Commands;

public class PostularEstudianteCommand: IRequest<bool>
{
    public CrearSolicitudDto Dto { get; }

    public PostularEstudianteCommand(CrearSolicitudDto dto)
    {
        Dto = dto;
    }
}