using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Interfaces;

public interface IProyectoService
{
    Task<IEnumerable<ProyectoEmpresaDTO>> ListarPorEmpresaAsync(uint idEmpresa);
}