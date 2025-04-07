using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class ResumenHistoricoVisita
{
    public int IdResumen { get; set; }

    public int IdPerfil { get; set; }

    public DateOnly PeriodoInicio { get; set; }

    public DateOnly PeriodoFin { get; set; }

    public int TotalVisitas { get; set; }

    public int TotalContactos { get; set; }

    public decimal TiempoPromedio { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;
}
