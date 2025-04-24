public class CrearProyectoDto
{
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    
    public string FechaLimite { get; set; } = null!; // <--dia/mes/año
    public int NumeroParticipantes { get; set; }
    public uint IdTipoRecompensa { get; set; }
    public ulong IdEmpresa { get; set; }  // este es el IdUsuario de la empresa
}