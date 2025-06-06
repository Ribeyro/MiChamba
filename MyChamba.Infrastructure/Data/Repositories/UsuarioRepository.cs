using Microsoft.EntityFrameworkCore;
using MyChamba.Data;
using MyChamba.Data.Repository;
using MyChamba.Data.UnitofWork;
using MyChamba.Domain.Models;
using MyChamba.Models;

namespace MyChamba.Repositories;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly MyChambaContext _context;

    public UsuarioRepository(MyChambaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
