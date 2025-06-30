using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Commands;

public class ListarProyectosPorEmpresaQueryHandler (IProyectoRepository _proyectoRepository): IRequestHandler<ListarProyectosPorEmpresaQuery, IEnumerable<ProyectoEmpresaDTO>>
{
    public async Task<IEnumerable<ProyectoEmpresaDTO>> Handle(ListarProyectosPorEmpresaQuery query, CancellationToken cancellationToken)
    {
        var proyectosRaw = await _proyectoRepository.ObtenerProyectosPorEmpresaAsync(query.IdEmpresa);

        var proyectosMapeados = proyectosRaw.Select(p => new ProyectoEmpresaDTO
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

        return proyectosMapeados;
    }
}