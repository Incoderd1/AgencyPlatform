using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class HistorialConfiguracion
{
    public int IdHistorial { get; set; }

    public int IdConfig { get; set; }

    public string? ValorAnterior { get; set; }

    public string? ValorNuevo { get; set; }

    public DateTime FechaCambio { get; set; }

    public int? RealizadoPor { get; set; }

    public string? Motivo { get; set; }

    public string? IpOrigen { get; set; }

    public virtual ConfiguracionSistema IdConfigNavigation { get; set; } = null!;

    public virtual Usuario? RealizadoPorNavigation { get; set; }
}
