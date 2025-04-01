using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class estadisticas_rendimiento
{
    public int id_estadistica { get; set; }

    public DateTime fecha_registro { get; set; }

    public string tipo_operacion { get; set; } = null!;

    public string tabla_afectada { get; set; } = null!;

    public int cantidad_registros { get; set; }

    public int tiempo_ejecucion { get; set; }

    public int? consumo_memoria { get; set; }

    public bool utilizacion_indices { get; set; }

    public string? plan_ejecucion { get; set; }

    public string? consulta_ejecutada { get; set; }
}
