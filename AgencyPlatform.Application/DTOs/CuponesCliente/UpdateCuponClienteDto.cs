using AgencyPlatform.Shared.Enums;

namespace AgencyPlatform.Application.DTOs.CuponesCliente
{
    public class UpdateCuponClienteDto
    {
        public DateTime? FechaUso { get; set; }
        public string? Estado { get; set; }
        public long? IdTransaccion { get; set; }
        public int? PuntosCanjeados { get; set; }
    }
}
