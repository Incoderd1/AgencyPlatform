using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class configuracion_sistema
{
    public int id_config { get; set; }

    public string clave { get; set; } = null!;

    public string valor { get; set; } = null!;

    public string tipo { get; set; } = null!;

    public string? descripcion { get; set; }

    public string grupo { get; set; } = null!;

    public bool modificable { get; set; }

    public string nivel_cache { get; set; } = null!;

    public int? tiempo_expiracion_cache { get; set; }

    public string entorno { get; set; } = null!;

    public DateTime fecha_actualizacion { get; set; }

    public int? actualizado_por { get; set; }

    public int version { get; set; }

    public virtual usuario? actualizado_porNavigation { get; set; }

    public virtual ICollection<historial_configuracion> historial_configuracions { get; set; } = new List<historial_configuracion>();
}
