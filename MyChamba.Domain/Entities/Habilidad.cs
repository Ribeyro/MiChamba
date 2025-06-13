namespace MyChamba.Domain.Entities
{
    public class Habilidad
    {
        public uint Id { get; set; }                  // Identificador único
        public string Nombre { get; set; } = string.Empty; // Nombre de la habilidad

        // Relación con Proyecto (si un proyecto tiene varias habilidades)
    
    }
}