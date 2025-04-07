using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class EstadisticasRendimiento
{
    public int IdEstadistica { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string TipoOperacion { get; set; } = null!;

    public string TablaAfectada { get; set; } = null!;

    public int CantidadRegistros { get; set; }

    public int TiempoEjecucion { get; set; }

    public int? ConsumoMemoria { get; set; }

    public bool UtilizacionIndices { get; set; }

    public string? PlanEjecucion { get; set; }

    public string? ConsultaEjecutada { get; set; }
}
