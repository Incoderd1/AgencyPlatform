using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class visitas_perfil_actual
{
    public int id_visita { get; set; }

    public int id_perfil { get; set; }

    public int? id_cliente { get; set; }

    public DateTime fecha_visita { get; set; }

    public string ip_visitante { get; set; } = null!;

    public string? user_agent { get; set; }

    public int? tiempo_visita { get; set; }

    public string? dispositivo { get; set; }

    public string? origen { get; set; }

    public string? region_geografica { get; set; }

    public virtual cliente? id_clienteNavigation { get; set; }

    public virtual perfile id_perfilNavigation { get; set; } = null!;
}
