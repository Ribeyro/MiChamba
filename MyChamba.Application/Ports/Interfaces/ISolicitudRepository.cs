using MyChamba.Infrastructure.Models;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface ISolicitudRepository : IGenericRepository<Solicitude>
{
    Task<Solicitude?> ObtenerPorEstudianteYProyectoAsync(ulong idEstudiante, uint idProyecto);
}