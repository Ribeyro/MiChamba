namespace MyChamba.Models;

public class RecompensasEconomica
{
    public uint Id { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public uint IdRecompensa { get; set; }

    public virtual Recompensa IdRecompensaNavigation { get; set; } = null!;
}