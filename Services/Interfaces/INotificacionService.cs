using MyChamba.Models;

namespace MyChamba.Services.Interfaces;

public interface INotificacionService
{
    Task CrearNotificacionNuevaSolicitudAsync(Solicitude solicitud, ulong idEmpresa, string resumenHabilidades);
}
