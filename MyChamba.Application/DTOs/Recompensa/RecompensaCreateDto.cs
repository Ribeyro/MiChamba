namespace MyChamba.DTOs.Recompensa;

public class RecompensaCreateDto
{
    public uint IdEstudiante { get; set; }
    public uint IdProyecto { get; set; }
    public uint IdTipoRecompensa { get; set; } // 1 = Económica, 2 = Certificado

    // Certificado
    public string? TipoCertificado { get; set; }
    public string? Archivo { get; set; }
    public DateTime? FechaEmision { get; set; }

    // Económica
    public decimal? Monto { get; set; }
    public string? MetodoPago { get; set; }
    public string? Estado { get; set; }
}