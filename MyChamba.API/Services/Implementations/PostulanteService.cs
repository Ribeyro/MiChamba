using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations
{
    public class PostulanteService (IPostulanteRepository _postulanteRepository): IPostulanteService
    {
        public async Task<List<PostulanteDto>> ObtenerPostulantesPorProyectoAsync(uint idProyecto)
        {
            var postulantes = await _postulanteRepository.ObtenerPostulantesPorProyectoAsync(idProyecto);

            var mapped = postulantes.Select(p => new PostulanteDto
            {
                IdSolicitud = p.IdSolicitud,
                IdUsuario = p.IdUsuario,
                NombreCompleto = p.NombreCompleto,
                Email = p.Email,
                Universidad = p.Universidad,
                Carrera = p.Carrera,
                AcercaDe = p.AcercaDe,
                EstadoSolicitud = p.EstadoSolicitud
            }).ToList();

            return mapped;
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
