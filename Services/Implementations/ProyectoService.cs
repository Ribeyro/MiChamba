using MyChamba.Data.Interface;
using MyChamba.DTOs.Proyecto;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class ProyectoService:IProyectoService
{
    private readonly IProyectoRepository _proyectoRepository;

    public ProyectoService(IProyectoRepository proyectoRepository)
    {
        _proyectoRepository = proyectoRepository;
    }

    public async Task<IEnumerable<ProyectoEmpresaDTO>> ListarPorEmpresaAsync(uint idEmpresa)
    {
        return await _proyectoRepository.ObtenerProyectosPorEmpresaAsync(idEmpresa);
    }
}