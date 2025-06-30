using MediatR;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Commands;

public class ListarProyectosPorEmpresaQuery : IRequest<IEnumerable<ProyectoEmpresaDTO>>
{
    public uint IdEmpresa { get; }

    public ListarProyectosPorEmpresaQuery(uint idEmpresa)
    {
        IdEmpresa = idEmpresa;
    }
}