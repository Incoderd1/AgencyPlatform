using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class CuponesCliente
{
    public int IdCuponCliente { get; set; }

    public long IdCupon { get; set; }

    public long IdCliente { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public DateTime? FechaUso { get; set; }

    public string Estado { get; set; } = null!;

    public long? IdTransaccion { get; set; }

    public int? PuntosCanjeados { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Cupone IdCuponNavigation { get; set; } = null!;
}
