namespace MyChamba.DTOs.Proyecto;

public class CrearProyectoDTO
{
    public ulong IdEmpresa { get; set; } // El id de la empresa que crea el proyecto
    public string Titulo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public DateTime FechaLimite { get; set; }
    public uint TipoRecompensa { get; set; }

    public List<uint> IdHabilidades { get; set; } = new(); // IDs de habilidades asociadas
}