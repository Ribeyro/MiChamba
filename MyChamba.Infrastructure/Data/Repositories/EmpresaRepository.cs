using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Empresas;
using MyChamba.Data;

namespace MyChamba.Infrastructure.Data.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly MyChambaContext _context;

    public EmpresaRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<EmpresaProfileDto?> GetProfileAsync(ulong idUsuario)
    {
        var empresa = await _context.Empresas
            .Include(e => e.IdSectorNavigation)
            .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);

        if (empresa == null)
            return null;

        return new EmpresaProfileDto
        {
            IdUsuario = empresa.IdUsuario,
            Nombre = empresa.Nombre,
            Telefono = empresa.Telefono,
            Direccion = empresa.Direccion,
            Ruc = empresa.Ruc,
            Logo = empresa.Logo,
            Sector = empresa.IdSectorNavigation.Nombre
        };
    }
}