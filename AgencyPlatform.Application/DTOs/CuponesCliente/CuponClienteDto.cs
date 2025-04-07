using AgencyPlatform.Shared.Enums;




namespace AgencyPlatform.Application.DTOs.CuponesCliente
{
    public class CuponClienteDto
    {
        public int IdCuponCliente { get; set; }
        public long IdCupon { get; set; }
        public long IdCliente { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaUso { get; set; }
        public string Estado { get; set; }
        public long? IdTransaccion { get; set; }
        public int? PuntosCanjeados { get; set; }
    }
}
