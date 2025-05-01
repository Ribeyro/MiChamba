using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Interfaces;

public interface IProyectoService
{
    Task<bool> CrearProyectoAsync(CrearProyectoDTO dto);
}