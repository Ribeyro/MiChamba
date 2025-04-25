// En MyChamba.Data.Repositories
using Microsoft.EntityFrameworkCore;
using MyChamba.Data;
using MyChamba.Models;

public class TipoRecompensaRepository : ITipoRecompensaRepository
{
    private readonly MyChambaContext _context;

    public TipoRecompensaRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<TipoRecompensa?> ObtenerPorIdAsync(uint id)
    {
        return await _context.TipoRecompensas.FindAsync(id);
    }
}