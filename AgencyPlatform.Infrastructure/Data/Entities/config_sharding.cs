using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class config_sharding
{
    public int id_config { get; set; }

    public string tabla { get; set; } = null!;

    public string metodo_sharding { get; set; } = null!;

    public string campo_sharding { get; set; } = null!;

    public int numero_shards { get; set; }

    public int shard_actual { get; set; }

    public string configuracion_shard { get; set; } = null!;

    public bool activo { get; set; }

    public DateTime fecha_creacion { get; set; }

    public DateTime fecha_actualizacion { get; set; }
}
