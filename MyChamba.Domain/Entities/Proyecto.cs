using MyChamba.Infrastructure.Models;

namespace MyChamba.Models;

public class Proyecto
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaLimite { get; set; }

    public bool Estado { get; set; }

    public int NumeroParticipantes { get; set; }

    public ulong IdEmpresa { get; set; }

    public uint IdTipoRecompensa { get; set; }

    public virtual ICollection<EntregasProyecto> EntregasProyectos { get; set; } = new List<EntregasProyecto>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual TipoRecompensa IdTipoRecompensaNavigation { get; set; } = null!;

    public virtual ICollection<Recompensa> Recompensas { get; set; } = new List<Recompensa>();

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();

    public virtual ICollection<Habilidade> IdHabilidads { get; set; } = new List<Habilidade>();
}