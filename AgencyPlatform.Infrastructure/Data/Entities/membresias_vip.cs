using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class membresias_vip
{
    public int id_plan { get; set; }

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public decimal precio_mensual { get; set; }

    public decimal? precio_trimestral { get; set; }

    public decimal? precio_anual { get; set; }

    public string beneficios { get; set; } = null!;

    public int puntos_mensuales { get; set; }

    public short reduccion_anuncios { get; set; }

    public short descuentos_adicionales { get; set; }

    public string estado { get; set; } = null!;

    public DateTime fecha_creacion { get; set; }

    public DateTime fecha_actualizacion { get; set; }

    public virtual ICollection<suscripciones_vip> suscripciones_vips { get; set; } = new List<suscripciones_vip>();
}
