﻿using MyChamba.Domain.Models;

namespace MyChamba.Models;

public class TipoUsuario
{
    public ulong Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}