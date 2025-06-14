using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Data;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Infrastructure.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly MyChambaContext _context;

    public StudentRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosCompletosAsync()
    {
        var proyectos = await _context.Proyectos
            .Include(p => p.IdTipoRecompensaNavigation)
            .Include(p => p.IdHabilidads)
            .ToListAsync();

        return proyectos.Select(p => new ProyectoCompletoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaLimite = p.FechaLimite,
            TipoRecompensa = p.IdTipoRecompensaNavigation?.Tipo,
            Habilidades = p.IdHabilidads.Select(h => new HabilidadDto
            {
                Id = h.Id,
                Nombre = h.Nombre
            }).ToList()
        }).ToList();
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto,
        uint? idEmpresa = null)
    {
        if (!DateTime.TryParseExact(fechaTexto, "dd/MM/yyyy", null, DateTimeStyles.None, out var fecha))
            throw new ArgumentException("La fecha debe estar en el formato dd/MM/yyyy.");

        var proyectos = await _context.Proyectos
            .Where(p => p.FechaLimite.Date == fecha.Date)
            .Include(p => p.IdTipoRecompensaNavigation)
            .Include(p => p.IdHabilidads)
            .ToListAsync();

        if (idEmpresa.HasValue) proyectos = proyectos.Where(p => p.IdEmpresa == idEmpresa.Value).ToList();

        return proyectos.Select(p => new ProyectoCompletoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaLimite = p.FechaLimite,
            TipoRecompensa = p.IdTipoRecompensaNavigation?.Tipo,
            Habilidades = p.IdHabilidads.Select(h => new HabilidadDto
            {
                Id = h.Id,
                Nombre = h.Nombre
            }).ToList()
        }).ToList();
    }
}