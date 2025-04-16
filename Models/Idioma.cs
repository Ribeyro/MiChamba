using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class Idioma
{
    public uint Id { get; set; }

    public string Idioma1 { get; set; } = null!;

    public virtual ICollection<EstudianteIdioma> EstudianteIdiomas { get; set; } = new List<EstudianteIdioma>();
}
