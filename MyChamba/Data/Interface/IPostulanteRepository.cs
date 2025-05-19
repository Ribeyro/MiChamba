using System.Collections.Generic;
using System.Threading.Tasks;
using MyChamba.DTOs.Student;
using MyChamba.Models;

namespace MyChamba.Data.Repositories
{
    public interface IPostulanteRepository
    {
        Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto);
        Task<Solicitude> ObtenerSolicitudPorIdAsync(uint idSolicitud);
        Task<List<Solicitude>> ObtenerOtrasSolicitudesDelProyectoAsync(uint idProyecto, uint idExcluido);
        Task GuardarCambiosAsync();
        void ActualizarSolicitud(Solicitude solicitud);
        void AgregarNotificaciones(List<Notificacione> notificaciones);
    }
}