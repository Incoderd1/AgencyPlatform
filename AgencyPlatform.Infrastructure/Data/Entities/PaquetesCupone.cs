using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class PaquetesCupone
{
    public int IdPaquete { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string Incluye { get; set; } = null!;

    public int PuntosOtorgados { get; set; }

    public bool SorteoIncluido { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? Stock { get; set; }

    public int Ventas { get; set; }

    public string Estado { get; set; } = null!;

    public string? ImagenUrl { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }
}
