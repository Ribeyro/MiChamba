using MyChamba.Domain.Models;
using MyChamba.Models;

namespace MyChamba.Infrastructure.Models;

public partial class Empresa
{
    public ulong IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Ruc { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public uint IdSector { get; set; }

    public virtual Sector IdSectorNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
