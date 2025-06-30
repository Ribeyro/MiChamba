using MyChamba.Application.DTOs.Empresas;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface IEmpresaRepository
{
    Task<EmpresaProfileDto?> GetProfileAsync(ulong idUsuario);
}