using MyChamba.Models;
using System.Threading.Tasks;

namespace MyChamba.Data.Interface
{
    public interface ISolicitudRepository
    {
        Task<Solicitude?> ObtenerPorEstudianteYProyectoAsync(ulong idEstudiante, uint idProyecto);
        Task AddAsync(Solicitude solicitud);
        Task SaveChangesAsync();
    }
}