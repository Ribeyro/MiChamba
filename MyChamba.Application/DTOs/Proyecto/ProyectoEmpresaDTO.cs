namespace MyChamba.DTOs.Proyecto;

public class ProyectoEmpresaDTO
{
    public uint Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public DateTime FechaLimite { get; set; }
    public int NumeroPostulaciones { get; set; }
    
    public List<PostulanteProyectoDTO> Postulantes { get; set; } = new(); 
}