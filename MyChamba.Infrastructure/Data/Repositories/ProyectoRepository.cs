using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Data;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Infrastructure.Data.Repositories;

public class ProyectoRepository (MyChambaContext _context): IProyectoRepository
{
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
                Nombre = $"{s.IdEstudianteNavigation.Nombre} {s.IdEstudianteNavigation.Apellido}",
                Universidad = s.IdEstudianteNavigation.IdUniversidadNavigation.Nombre,
                Carrera = s.IdEstudianteNavigation.IdCarreraNavigation.Nombre,
                EstadoSolicitud = s.Estado
            }).ToList()

        }).ToList();
    }

}