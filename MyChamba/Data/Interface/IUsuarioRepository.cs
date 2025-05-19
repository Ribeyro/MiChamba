using MyChamba.Models;

namespace MyChamba.Data.UnitofWork;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);

}