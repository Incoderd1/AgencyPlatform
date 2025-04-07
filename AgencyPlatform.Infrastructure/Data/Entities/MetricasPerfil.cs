using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class MetricasPerfil
{
    public int IdMetrica { get; set; }

    public int IdPerfil { get; set; }

    public DateOnly Fecha { get; set; }

    public int Visitas { get; set; }

    public int Contactos { get; set; }

    public decimal TiempoPromedio { get; set; }

    public int PosicionRanking { get; set; }

    public string Tendencia { get; set; } = null!;

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;
}
