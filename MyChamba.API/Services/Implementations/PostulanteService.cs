using MyChamba.Application.DTOs.Student;
using MyChamba.Application.Services.Interfaces;
using MyChamba.Domain.Interface;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations
{
    public class PostulanteService : IPostulanteService
    {
        private readonly IPostulanteRepository _postulanteRepository;

        public PostulanteService(IPostulanteRepository postulanteRepository)
        {
            _postulanteRepository = postulanteRepository;
        }

        public async Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto)
        {
            return await _postulanteRepository.ObtenerPostulantesPorProyectoAsync(idProyecto);
        }

        public async Task AceptarPostulanteAsync(uint idSolicitud)
        {
            var solicitudAceptada = await _postulanteRepository.ObtenerSolicitudPorIdAsync(idSolicitud);
            if (solicitudAceptada == null)
                throw new Exception("Solicitud no encontrada.");

            var otrasSolicitudes = await _postulanteRepository.ObtenerOtrasSolicitudesDelProyectoAsync(solicitudAceptada.IdProyecto, idSolicitud);

            solicitudAceptada.Estado = "aceptada";
            _postulanteRepository.ActualizarSolicitud(solicitudAceptada);

            var notificaciones = new List<Notificacione>();

            foreach (var s in otrasSolicitudes)
            {
                s.Estado = "rechazada";
                _postulanteRepository.ActualizarSolicitud(s);
                notificaciones.Add(new Notificacione
                {
                    IdReceptor = s.IdEstudiante,
                    IdSolicitud = s.Id,
                    Mensaje = "No has sido seleccionado para el reto.",
                    TipoMensaje = "Postulación",
                    FechaEnvio = DateTime.UtcNow,
                    Leido = false
                });
            }

            notificaciones.Add(new Notificacione
            {
                IdReceptor = solicitudAceptada.IdEstudiante,
                IdSolicitud = solicitudAceptada.Id,
                Mensaje = "¡Felicidades! Has sido seleccionado para el reto.",
                TipoMensaje = "Postulación",
                FechaEnvio = DateTime.UtcNow,
                Leido = false
            });

            _postulanteRepository.AgregarNotificaciones(notificaciones);
            await _postulanteRepository.GuardarCambiosAsync();
        }
    }
}
