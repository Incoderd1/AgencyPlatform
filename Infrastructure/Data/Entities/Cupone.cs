using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class Cupone
{
    public int IdCupon { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string TipoDescuento { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public decimal? MinimoCompra { get; set; }

    public decimal? MaximoDescuento { get; set; }

    public int? UsosMaximos { get; set; }

    public int UsosActuales { get; set; }

    public int UsosPorCliente { get; set; }

    public string AplicaA { get; set; } = null!;

    public int? RequierePuntos { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }
}
