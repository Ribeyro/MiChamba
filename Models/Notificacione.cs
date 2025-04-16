using System;
using System.Collections.Generic;

namespace MyChamba.Models;

public partial class Notificacione
{
    public uint Id { get; set; }

    public uint IdSolicitud { get; set; }

    public ulong IdReceptor { get; set; }

    public string TipoMensaje { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

    public string Mensaje { get; set; } = null!;

    public bool Leido { get; set; }

    public virtual Usuario IdReceptorNavigation { get; set; } = null!;

    public virtual Solicitude IdSolicitudNavigation { get; set; } = null!;
}
