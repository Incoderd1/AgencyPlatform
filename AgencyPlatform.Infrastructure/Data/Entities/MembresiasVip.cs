using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class MembresiasVip
{
    public int IdPlan { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioMensual { get; set; }

    public decimal? PrecioTrimestral { get; set; }

    public decimal? PrecioAnual { get; set; }

    public string Beneficios { get; set; } = null!;

    public int PuntosMensuales { get; set; }

    public short ReduccionAnuncios { get; set; }

    public short DescuentosAdicionales { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual ICollection<SuscripcionesVip> SuscripcionesVips { get; set; } = new List<SuscripcionesVip>();
}
