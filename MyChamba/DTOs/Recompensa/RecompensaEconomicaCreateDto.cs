namespace MyChamba.DTOs.Recompensa;

public class RecompensaEconomicaCreateDto
{
    public uint IdRecompensa { get; set; } // Ya debe existir
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; } = null!;
    public string Estado { get; set; } = null!;
}
