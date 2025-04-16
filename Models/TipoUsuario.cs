using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class TipoUsuario
{
    public ulong Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
