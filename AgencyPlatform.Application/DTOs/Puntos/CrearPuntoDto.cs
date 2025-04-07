using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Puntos
{
    public class CrearPuntoDto
    {
        public int IdCliente { get; set; }
        public int Cantidad { get; set; }
        public string TipoAccion { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public long? IdReferencia { get; set; }
        public string? TipoReferencia { get; set; }
        public decimal Multiplicador { get; set; } = 1.00m;
    }
}
