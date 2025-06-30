using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Data;
using MyChamba.Data.Repository;
using MyChamba.Domain.Models;

namespace MyChamba.Repositories;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly MyChambaContext _context;

    public UsuarioRepository(MyChambaContext context) : base(context) // 👈 Llamada al constructor base
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }

    public async Task AgregarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }
}