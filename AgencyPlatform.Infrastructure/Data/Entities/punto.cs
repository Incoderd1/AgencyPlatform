using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class punto
{
    public int id_punto { get; set; }

    public int id_cliente { get; set; }

    public int cantidad { get; set; }

    public string tipo_accion { get; set; } = null!;

    public string? descripcion { get; set; }

    public DateTime fecha_obtencion { get; set; }

    public DateTime? fecha_expiracion { get; set; }

    public string estado { get; set; } = null!;

    public long? id_referencia { get; set; }

    public string? tipo_referencia { get; set; }

    public decimal multiplicador { get; set; }

    public virtual cliente id_clienteNavigation { get; set; } = null!;
}
