using Microsoft.EntityFrameworkCore;
using MyChamba.Data.Interface;
using MyChamba.Models;

namespace MyChamba.Data.Repositories
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly MyChambaContext _context;

        public SolicitudRepository(MyChambaContext context)
        {
            _context = context;
        }

        public async Task<Solicitude?> ObtenerPorEstudianteYProyectoAsync(ulong idEstudiante, uint idProyecto)
        {
            return await _context.Solicitudes
                .FirstOrDefaultAsync(s => s.IdEstudiante == idEstudiante && s.IdProyecto == idProyecto);
        }

        public async Task AddAsync(Solicitude solicitud)
        {
            await _context.Solicitudes.AddAsync(solicitud);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}