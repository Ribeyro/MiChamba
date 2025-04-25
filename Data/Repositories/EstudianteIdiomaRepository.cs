using Microsoft.EntityFrameworkCore;
using MyChamba.Data.Interface;
using MyChamba.Models;

namespace MyChamba.Data.Repositories
{
    public class EstudianteIdiomaRepository : IEstudianteIdiomaRepository
    {
        private readonly MyChambaContext _context;

        public EstudianteIdiomaRepository(MyChambaContext context)
        {
            _context = context;
        }

        public async Task<List<EstudianteIdioma>> ObtenerPorEstudianteAsync(ulong estudianteId)
        {
            return await _context.EstudianteIdiomas
                .Include(ei => ei.IdIdiomaNavigation)
                .Where(ei => ei.IdEstudiante == estudianteId)
                .ToListAsync();
        }

        public async Task AgregarRangoAsync(List<EstudianteIdioma> idiomas)
        {
            await _context.EstudianteIdiomas.AddRangeAsync(idiomas);
            await _context.SaveChangesAsync();
        }
    }
}