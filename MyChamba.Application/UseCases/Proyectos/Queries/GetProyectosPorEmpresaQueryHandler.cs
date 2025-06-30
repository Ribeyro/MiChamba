using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Queries;

public class GetProyectosPorEmpresaQueryHandler (IProyectoRepository _proyectoRepository)
    : IRequestHandler<GetProyectosPorEmpresaQuery, IEnumerable<ProyectoEmpresaDTO>>
{
    public async Task<IEnumerable<ProyectoEmpresaDTO>> Handle(GetProyectosPorEmpresaQuery query, CancellationToken cancellationToken)
    {
        var proyectosRaw = await _proyectoRepository.ObtenerProyectosPorEmpresaAsync(query.IdEmpresa);

        return proyectosRaw.Select(p => new ProyectoEmpresaDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaLimite = p.FechaLimite,
            NumeroPostulaciones = p.NumeroPostulaciones,
            Postulantes = p.Postulantes.Select(post => new PostulanteProyectoDTO
            {
                IdEstudiante = post.IdEstudiante,
                Nombre = $"{post.Nombre} {post.Apellido}",
                Universidad = post.Universidad,
                Carrera = post.Carrera
            }).ToList()
        });
    }
}