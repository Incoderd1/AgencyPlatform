using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class AccionesAntifrauide
{
    public int IdAccion { get; set; }

    public string TipoEntidad { get; set; } = null!;

    public int IdEntidad { get; set; }

    public string TipoAccion { get; set; } = null!;

    public string Motivo { get; set; } = null!;

    public string? Evidencia { get; set; }

    public string? IpRelacionada { get; set; }

    public DateTime FechaDeteccion { get; set; }

    public DateTime FechaAccion { get; set; }

    public int? EjecutadaPor { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaResolucion { get; set; }

    public virtual Usuario? EjecutadaPorNavigation { get; set; }
}
