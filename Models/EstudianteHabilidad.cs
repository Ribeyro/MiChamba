﻿using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class EstudianteHabilidad
{
    public ulong Id { get; set; }

    public ulong IdEstudiante { get; set; }

    public uint IdHabilidad { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Habilidade IdHabilidadNavigation { get; set; } = null!;
}
