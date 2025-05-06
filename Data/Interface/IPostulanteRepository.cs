using System.Collections.Generic;
using System.Threading.Tasks;
using MyChamba.DTOs.Student;

namespace MyChamba.Data.Repositories
{
    public interface IPostulanteRepository
    {
        Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto);
        Task AceptarPostulanteAsync(uint idSolicitud);
    }
}