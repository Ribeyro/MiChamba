using MediatR;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Queries;

public class GetProyectosPorEmpresaQuery : IRequest<IEnumerable<ProyectoEmpresaDTO>>
{
    public uint IdEmpresa { get; }

    public GetProyectosPorEmpresaQuery(uint idEmpresa)
    {
        IdEmpresa = idEmpresa;
    }
}