using MyChamba.Domain.Models;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface IUsuarioRepository
{
    
    Task<Usuario?> GetByEmailAsync(string email); // ✅ Aquí agregamos el método
}