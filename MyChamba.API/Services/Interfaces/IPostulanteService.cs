using MyChamba.Application.DTOs.Student;
using MyChamba.DTOs.Student;

namespace MyChamba.Application.Services.Interfaces
{
    public interface IPostulanteService
    {
        Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto);
        Task AceptarPostulanteAsync(uint idSolicitud);
    }
}