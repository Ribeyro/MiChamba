using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class PerfilEstudiante
{
    public ulong IdEstudiante { get; set; }

    public string AcercaDe { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;
}
