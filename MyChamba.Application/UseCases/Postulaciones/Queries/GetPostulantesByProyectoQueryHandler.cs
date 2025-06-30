using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;

namespace MyChamba.Application.UseCases.Postulaciones.Queries;

public class GetPostulantesByProyectoQueryHandler (IPostulanteRepository _postulanteRepository)
    : IRequestHandler<GetPostulantesByProyectoQuery, List<PostulanteDto>>
{
    
    public async Task<List<PostulanteDto>> Handle(GetPostulantesByProyectoQuery query, CancellationToken cancellationToken)
    {
        var postulantes = await _postulanteRepository.ObtenerPostulantesPorProyectoAsync(query.IdProyecto);

        return postulantes.Select(p => new PostulanteDto
        {
            IdSolicitud = p.IdSolicitud,
            IdUsuario = p.IdUsuario,
            NombreCompleto = p.NombreCompleto,
            Email = p.Email,
            Universidad = p.Universidad,
            Carrera = p.Carrera,
            AcercaDe = p.AcercaDe,
            EstadoSolicitud = p.EstadoSolicitud
        }).ToList();
    }
}