using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class metricas_perfil
{
    public int id_metrica { get; set; }

    public int id_perfil { get; set; }

    public DateOnly fecha { get; set; }

    public int visitas { get; set; }

    public int contactos { get; set; }

    public decimal tiempo_promedio { get; set; }

    public int posicion_ranking { get; set; }

    public string tendencia { get; set; } = null!;

    public virtual perfile id_perfilNavigation { get; set; } = null!;
}
