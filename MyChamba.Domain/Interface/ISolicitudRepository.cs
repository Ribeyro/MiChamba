using MyChamba.Data.Interface;
using MyChamba.Infrastructure.Models;

namespace MyChamba.Domain.Interface;

public interface ISolicitudRepository : IGenericRepository<Solicitude>
{
    Task<Solicitude?> ObtenerPorEstudianteYProyectoAsync(ulong idEstudiante, uint idProyecto);
}