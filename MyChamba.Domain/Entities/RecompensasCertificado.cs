﻿namespace MyChamba.Models;

public class RecompensasCertificado
{
    public uint IdRecompensa { get; set; }

    public string Tipo { get; set; } = null!;

    public string Archivo { get; set; } = null!;

    public DateTime FechaEmision { get; set; }

    public virtual Recompensa IdRecompensaNavigation { get; set; } = null!;
}