using MyChamba.Models;
using System.Threading.Tasks;
using MyChamba.Data.Interface;

namespace MyChamba.Data.UnitofWork;

public interface ISolicitudRepository : IGenericRepository<Solicitude>
{
    Task<Solicitude?> ObtenerPorEstudianteYProyectoAsync(ulong idEstudiante, uint idProyecto);
}