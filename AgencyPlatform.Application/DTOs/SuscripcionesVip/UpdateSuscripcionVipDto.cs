using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.SuscripcionesVip
{
    public class UpdateSuscripcionVipDto
    {
        public DateTime? FechaRenovacion { get; set; }
        public DateTime? FechaProximoCargo { get; set; }
        public decimal? MontoDescuento { get; set; }
        public decimal? Impuestos { get; set; }
        public decimal? MontoPagado { get; set; }
        public bool? AutoRenovacion { get; set; }
        public int? IntentosCobro { get; set; }
        public DateTime? FechaUltimoIntento { get; set; }
        public string? Estado { get; set; }
        public string? CuponAplicado { get; set; }
        public string? MetodoPago { get; set; }
        public string? GatewayPago { get; set; }
        public string? IdClienteGateway { get; set; }
        public string? IdSuscripcionGateway { get; set; }
        public DatosPagoDto? DatosPago { get; set; }
        public string? ReferenciaPago { get; set; }
        public string? IdTransaccionPago { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public DateTime? EfectivaHasta { get; set; }
        public string? MotivoCancelacion { get; set; }
        public string? NotasInternas { get; set; }
        public bool? HaRecibidoRecordatorio { get; set; }
    }
}
