using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Student;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Postulaciones.AceptarPostulante;


public interface IAceptarPostulanteUseCase
{
    Task ExecuteAsync(uint idSolicitud);
}

public class AceptarPostulanteUseCase(IPostulanteRepository _postulanteRepository)
    : IAceptarPostulanteUseCase
{
    public async Task ExecuteAsync(uint idSolicitud)
    {
        var solicitudAceptada = await _postulanteRepository.ObtenerSolicitudPorIdAsync(idSolicitud);
        if (solicitudAceptada == null)
            throw new Exception("Solicitud no encontrada.");

        var otrasSolicitudes =
            await _postulanteRepository.ObtenerOtrasSolicitudesDelProyectoAsync(solicitudAceptada.IdProyecto, idSolicitud);

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