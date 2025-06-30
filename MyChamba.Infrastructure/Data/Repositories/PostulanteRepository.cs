using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Data;
using MyChamba.Domain.DTOs;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Infrastructure.Data.Repositories;

public class PostulanteRepository : IPostulanteRepository
{
    private readonly MyChambaContext _context;

    public PostulanteRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto)
    {
        var query = from s in _context.Solicitudes
            join e in _context.Estudiantes on s.IdEstudiante equals e.IdUsuario
            join u in _context.Usuarios on e.IdUsuario equals u.Id
            join c in _context.Carreras on e.IdCarrera equals c.Id into carreraJoin
            from carrera in carreraJoin.DefaultIfEmpty()
            join un in _context.Universidads on e.IdUniversidad equals un.Id into uniJoin
            from universidad in uniJoin.DefaultIfEmpty()
            join p in _context.PerfilEstudiantes on e.IdUsuario equals p.IdEstudiante into perfilJoin
            from perfil in perfilJoin.DefaultIfEmpty()
            where s.IdProyecto == idProyecto
            select new PostulanteDto
            {
                IdSolicitud = s.Id,
                IdUsuario = u.Id,
                NombreCompleto = e.Nombre + " " + e.Apellido,
                Email = u.Email,
                Universidad = universidad != null ? universidad.Nombre : "",
                Carrera = carrera != null ? carrera.Nombre : "",
                AcercaDe = perfil != null ? perfil.AcercaDe : "",
                EstadoSolicitud = s.Estado
            };

        return await query.ToListAsync();
    }

    public async Task<Solicitude> ObtenerSolicitudPorIdAsync(uint idSolicitud)
    {
        return await _context.Solicitudes.FirstOrDefaultAsync(s => s.Id == idSolicitud);
    }

    public async Task<List<Solicitude>> ObtenerOtrasSolicitudesDelProyectoAsync(uint idProyecto, uint idExcluido)
    {
        return await _context.Solicitudes
            .Where(s => s.IdProyecto == idProyecto && s.Id != idExcluido)
            .ToListAsync();
    }

    public void ActualizarSolicitud(Solicitude solicitud)
    {
        _context.Solicitudes.Update(solicitud);
    }

    public void AgregarNotificaciones(List<Notificacione> notificaciones)
    {
        _context.Notificaciones.AddRange(notificaciones);
    }

    public async Task GuardarCambiosAsync()
    {
        await _context.SaveChangesAsync();
    }
}