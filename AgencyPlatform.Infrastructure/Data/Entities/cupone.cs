using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class cupone
{
    public int id_cupon { get; set; }

    public string codigo { get; set; } = null!;

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public string tipo_descuento { get; set; } = null!;

    public decimal valor { get; set; }

    public DateTime fecha_inicio { get; set; }

    public DateTime fecha_fin { get; set; }

    public decimal? minimo_compra { get; set; }

    public decimal? maximo_descuento { get; set; }

    public int? usos_maximos { get; set; }

    public int usos_actuales { get; set; }

    public int usos_por_cliente { get; set; }

    public string aplica_a { get; set; } = null!;

    public int? requiere_puntos { get; set; }

    public string estado { get; set; } = null!;

    public DateTime fecha_creacion { get; set; }
}
