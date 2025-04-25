using Microsoft.EntityFrameworkCore;
using MyChamba.Models;

namespace MyChamba.Data.Repositories;

public class ProyectoRepository : IProyectoRepository
{
    private readonly MyChambaContext _context;

    public ProyectoRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<Proyecto?> ObtenerPorIdAsync(uint id)
    {
        return await _context.Proyectos.FindAsync(id);
    }

    public async Task AgregarProyectoAsync(Proyecto proyecto)
    {
        await _context.Proyectos.AddAsync(proyecto);
    }
    
    public async Task<List<Proyecto>> ObtenerProyectosConDetallesAsync()
    {
        return await _context.Proyectos
            .Include(p => p.IdTipoRecompensaNavigation)  // Incluir TipoRecompensa
            .Include(p => p.IdHabilidads)  // Incluir las Habilidades
            .ToListAsync();
    }
    
    public async Task<List<Proyecto>> ObtenerProyectosConDetallesPorFechaYEmpresaAsync(DateTime fecha, uint idEmpresa)
    {
        return await _context.Proyectos
            .Where(p => p.FechaLimite.Date == fecha.Date && p.IdEmpresa == idEmpresa)
            .Include(p => p.IdTipoRecompensaNavigation)
            .Include(p => p.IdHabilidads)
            .ToListAsync();
    }


    
    
    


}