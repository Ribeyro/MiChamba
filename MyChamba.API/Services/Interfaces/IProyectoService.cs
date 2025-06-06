using MyChamba.DTOs.Proyecto;

namespace MyChamba.Application.Services.Interfaces;

/// <summary>
/// Interface que define las operaciones disponibles para el servicio de proyectos.
/// </summary>
public interface IProyectoService
{
    Task<IEnumerable<ProyectoEmpresaDTO>> ListarPorEmpresaAsync(uint idEmpresa);
    Task<bool> CrearProyectoAsync(CrearProyectoDTO dto);
}
