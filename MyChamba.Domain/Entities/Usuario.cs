using MyChamba.Infrastructure.Models;
using MyChamba.Models;

namespace MyChamba.Domain.Models;

public class Usuario
{
    public ulong Id { get; set; }

    public ulong IdTipoUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Empresa? Empresa { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
  
    public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();
}