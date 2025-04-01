using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class acciones_antifrauide
{
    public int id_accion { get; set; }

    public string tipo_entidad { get; set; } = null!;

    public int id_entidad { get; set; }

    public string tipo_accion { get; set; } = null!;

    public string motivo { get; set; } = null!;

    public string? evidencia { get; set; }

    public string? ip_relacionada { get; set; }

    public DateTime fecha_deteccion { get; set; }

    public DateTime fecha_accion { get; set; }

    public int? ejecutada_por { get; set; }

    public string estado { get; set; } = null!;

    public DateTime? fecha_resolucion { get; set; }

    public virtual usuario? ejecutada_porNavigation { get; set; }
}
