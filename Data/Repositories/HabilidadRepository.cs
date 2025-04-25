using MyChamba.Data.Interface;
using Microsoft.EntityFrameworkCore;
using MyChamba.Models;

namespace MyChamba.Data.Repositories;

public class HabilidadRepository : IHabilidadRepository
{
    private readonly MyChambaContext _context;

    public HabilidadRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<Habilidade?> ObtenerPorIdAsync(uint id)
    {
        return await _context.Habilidades.FindAsync(id);
    }

    public async Task<IEnumerable<Habilidade>> ObtenerPorIdsAsync(IEnumerable<uint> ids)
    {
        return await _context.Habilidades.Where(h => ids.Contains(h.Id)).ToListAsync();
    }
}