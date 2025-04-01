using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class cache_consulta
{
    public int id_cache { get; set; }

    public string clave_cache { get; set; } = null!;

    public string tipo_consulta { get; set; } = null!;

    public string datos { get; set; } = null!;

    public string? parametros_consulta { get; set; }

    public int tiempo_generacion { get; set; }

    public DateTime fecha_creacion { get; set; }

    public DateTime fecha_expiracion { get; set; }

    public int veces_utilizado { get; set; }

    public DateTime? ultimo_uso { get; set; }
}
