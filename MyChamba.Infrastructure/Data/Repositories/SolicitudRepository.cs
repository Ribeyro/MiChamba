using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Data;
using MyChamba.Data.Repository;
using MyChamba.Infrastructure.Models;

namespace MyChamba.Infrastructure.Data.Repositories;

public class SolicitudRepository : GenericRepository<Solicitude>, ISolicitudRepository
{
    private readonly MyChambaContext _context;

    public SolicitudRepository(MyChambaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Solicitude?> ObtenerPorEstudianteYProyectoAsync(ulong idEstudiante, uint idProyecto)
    {
        return await _context.Solicitudes
            .FirstOrDefaultAsync(s => s.IdEstudiante == idEstudiante && s.IdProyecto == idProyecto);
    }
}