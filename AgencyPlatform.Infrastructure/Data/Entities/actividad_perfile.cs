using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class actividad_perfile
{
    public int id_actividad { get; set; }

    public int id_perfil { get; set; }

    public string tipo_actividad { get; set; } = null!;

    public DateTime fecha_inicio { get; set; }

    public DateTime? fecha_fin { get; set; }

    public string? ip_registro { get; set; }

    public string? localizacion { get; set; }

    public string? notas { get; set; }

    public virtual perfile id_perfilNavigation { get; set; } = null!;
}
