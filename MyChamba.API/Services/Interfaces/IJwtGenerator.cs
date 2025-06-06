using MyChamba.Domain.Models;
using MyChamba.Models;

namespace MyChamba.Services.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(Usuario usuario);
}