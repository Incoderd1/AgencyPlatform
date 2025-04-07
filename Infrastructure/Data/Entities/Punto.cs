using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class Punto
{
    public int IdPunto { get; set; }

    public int IdCliente { get; set; }

    public int Cantidad { get; set; }

    public string TipoAccion { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaObtencion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public string Estado { get; set; } = null!;

    public long? IdReferencia { get; set; }

    public string? TipoReferencia { get; set; }

    public decimal Multiplicador { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
