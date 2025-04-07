using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class CacheConsulta
{
    public int IdCache { get; set; }

    public string ClaveCache { get; set; } = null!;

    public string TipoConsulta { get; set; } = null!;

    public string Datos { get; set; } = null!;

    public string? ParametrosConsulta { get; set; }

    public int TiempoGeneracion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public int VecesUtilizado { get; set; }

    public DateTime? UltimoUso { get; set; }
}
