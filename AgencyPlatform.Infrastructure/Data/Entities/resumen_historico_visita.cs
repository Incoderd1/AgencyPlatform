using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class resumen_historico_visita
{
    public int id_resumen { get; set; }

    public int id_perfil { get; set; }

    public DateOnly periodo_inicio { get; set; }

    public DateOnly periodo_fin { get; set; }

    public int total_visitas { get; set; }

    public int total_contactos { get; set; }

    public decimal tiempo_promedio { get; set; }

    public DateTime fecha_creacion { get; set; }

    public virtual perfile id_perfilNavigation { get; set; } = null!;
}
