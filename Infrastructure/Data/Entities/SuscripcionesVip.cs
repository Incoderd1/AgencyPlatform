using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class SuscripcionesVip
{
    public int IdSuscripcion { get; set; }

    public int IdCliente { get; set; }

    public int IdPlan { get; set; }

    public string NumeroSuscripcion { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public DateTime? FechaRenovacion { get; set; }

    public DateTime? FechaProximoCargo { get; set; }

    public string TipoCiclo { get; set; } = null!;

    public decimal MontoBase { get; set; }

    public decimal MontoDescuento { get; set; }

    public decimal Impuestos { get; set; }

    public decimal MontoPagado { get; set; }

    public string Moneda { get; set; } = null!;

    public bool AutoRenovacion { get; set; }

    public int IntentosCobro { get; set; }

    public DateTime? FechaUltimoIntento { get; set; }

    public string Estado { get; set; } = null!;

    public string OrigenSuscripcion { get; set; } = null!;

    public string? CuponAplicado { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string GatewayPago { get; set; } = null!;

    public string? IdClienteGateway { get; set; }

    public string? IdSuscripcionGateway { get; set; }

    public string? DatosPago { get; set; }

    public string? ReferenciaPago { get; set; }

    public string? IdTransaccionPago { get; set; }

    public DateTime? FechaCancelacion { get; set; }

    public DateTime? EfectivaHasta { get; set; }

    public string? MotivoCancelacion { get; set; }

    public string SolicitadaPor { get; set; } = null!;

    public string? NotasInternas { get; set; }

    public bool HaRecibidoRecordatorio { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual MembresiasVip IdPlanNavigation { get; set; } = null!;
}
