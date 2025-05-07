using System.Collections.Generic;
using System.Threading.Tasks;
using MyChamba.DTOs.Student;

namespace MyChamba.Services.Interfaces
{
    public interface IPostulanteService
    {
        Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto);
        Task AceptarPostulanteAsync(uint idSolicitud);
    }
}