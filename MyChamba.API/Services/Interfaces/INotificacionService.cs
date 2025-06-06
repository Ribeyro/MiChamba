using MyChamba.DTOs.Notificaciones;
using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Services.Interfaces;

public interface INotificacionService
{
    Task CrearNotificacionNuevaSolicitudAsync(Solicitude solicitud, ulong idEmpresa, string resumenHabilidades);
    Task<IEnumerable<NotificacionDto>> ObtenerPorEmpresaAsync(ulong idEmpresa);
    Task<IEnumerable<NotificacionDto>> ObtenerPorEstudianteAsync(ulong idEstudiante);
}
