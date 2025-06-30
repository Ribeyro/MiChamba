namespace MyChamba.DTOs.Recompensa;

public class RecompensaDto
{
    public uint Id { get; set; }
    public ulong IdEstudiante { get; set; }
    public uint IdProyecto { get; set; }
    public uint IdTipoRecompensa { get; set; }
    public DateTime FechaAsignacion { get; set; }
}