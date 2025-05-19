namespace MyChamba.DTOs.Recompensa;

public class RecompensaEconomicaUpdateDto
{
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
}