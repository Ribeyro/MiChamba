
using MyChamba.Domain.Entities;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Domain.Interface
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