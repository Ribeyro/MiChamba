using Microsoft.EntityFrameworkCore;
using MyChamba.Data.Context;
using MyChamba.Data.Interface;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Data.Repositories;

public class ProyectoRepository: IProyectoRepository
{
    private readonly MyChambaContext _context;

    public ProyectoRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProyectoEmpresaDTO>> ObtenerProyectosPorEmpresaAsync(uint idEmpresa)
    {
        var proyectos = await _context.Proyectos
            .Include(p => p.Solicitudes)
            .ThenInclude(s => s.IdEstudianteNavigation)
            .ThenInclude(e => e.IdCarreraNavigation)
            .Include(p => p.Solicitudes)
            .ThenInclude(s => s.IdEstudianteNavigation)
            .ThenInclude(e => e.IdUniversidadNavigation)
            .Where(p => p.IdEmpresa == idEmpresa)
            .ToListAsync();

        return proyectos.Select(p => new ProyectoEmpresaDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaLimite = p.FechaLimite,
            NumeroPostulaciones = p.Solicitudes.Count,
            Postulantes = p.Solicitudes.Select(s => new PostulanteProyectoDTO
            {
                IdEstudiante = (uint)s.IdEstudiante,
                Nombre = s.IdEstudianteNavigation.Nombre,
                Apellido = s.IdEstudianteNavigation.Apellido,
                Carrera = s.IdEstudianteNavigation.IdCarreraNavigation.Nombre,
                Universidad = s.IdEstudianteNavigation.IdUniversidadNavigation.Nombre,
                EstadoSolicitud = s.Estado
            }).ToList()
        }).ToList();
    }
}