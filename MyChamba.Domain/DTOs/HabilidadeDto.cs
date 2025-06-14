namespace MyChamba.Domain.Entities
{
    public class HabilidadeDto
    {
        public uint Id { get; set; }                  // Identificador único
        public string Nombre { get; set; } = string.Empty; // Nombre de la habilidad

        // Relación con ProyectoDto (si un proyecto tiene varias habilidades)
    
    }
}