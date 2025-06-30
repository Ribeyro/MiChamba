using MediatR;

namespace MyChamba.Application.UseCases.Postulaciones.Commands;

public class AceptarPostulanteCommand: IRequest<Unit>
{
    public uint IdSolicitud { get; }

    public AceptarPostulanteCommand(uint idSolicitud)
    {
        IdSolicitud = idSolicitud;
    }
}