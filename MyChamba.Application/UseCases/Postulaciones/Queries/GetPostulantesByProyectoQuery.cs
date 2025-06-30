using MediatR;
using MyChamba.Application.DTOs.Student;

namespace MyChamba.Application.UseCases.Postulaciones.Queries;

public class GetPostulantesByProyectoQuery: IRequest<List<PostulanteDto>>
{
    public uint IdProyecto { get; }

    public GetPostulantesByProyectoQuery(uint idProyecto)
    {
        IdProyecto = idProyecto;
    }
}