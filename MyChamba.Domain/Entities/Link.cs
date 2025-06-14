using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class Link
{
    public uint Id { get; set; }

    public ulong IdEstudiante { get; set; }

    public string TipoLink { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;
}
