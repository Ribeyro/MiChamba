using System;
using System.Collections.Generic;
using MyChamba.Domain.Models;
using MyChamba.Infrastructure.Models;

namespace MyChamba.Models;

public partial class Estudiante
{
    public ulong IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public uint IdUniversidad { get; set; }

    public uint IdCarrera { get; set; }

    public virtual ICollection<EntregasProyecto> EntregasProyectos { get; set; } = new List<EntregasProyecto>();

    public virtual ICollection<EstudianteHabilidad> EstudianteHabilidads { get; set; } = new List<EstudianteHabilidad>();

    public virtual ICollection<EstudianteIdioma> EstudianteIdiomas { get; set; } = new List<EstudianteIdioma>();

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual Universidad IdUniversidadNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual PerfilEstudiante? PerfilEstudiante { get; set; }

    public virtual ICollection<Recompensa> Recompensas { get; set; } = new List<Recompensa>();

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();
}
