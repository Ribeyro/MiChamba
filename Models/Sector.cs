using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class Sector
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();
}
