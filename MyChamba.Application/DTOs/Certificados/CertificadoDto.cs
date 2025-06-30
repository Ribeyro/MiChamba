namespace MyChamba.Application.DTOs.Certificados;

public class CertificadoDto
{
    public uint IdRecompensa { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Archivo { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
}