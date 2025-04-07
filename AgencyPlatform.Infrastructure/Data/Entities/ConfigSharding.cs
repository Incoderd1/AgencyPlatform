using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class ConfigSharding
{
    public int IdConfig { get; set; }

    public string Tabla { get; set; } = null!;

    public string MetodoSharding { get; set; } = null!;

    public string CampoSharding { get; set; } = null!;

    public int NumeroShards { get; set; }

    public int ShardActual { get; set; }

    public string ConfiguracionShard { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }
}
