using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Puntos
{
    public class PuntoDto
    {
        public int IdPunto { get; set; }
        public long IdCliente { get; set; }
        public int Cantidad { get; set; }
        public string TipoAccion { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime FechaObtencion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string Estado { get; set; } = null!;
        public long? IdReferencia { get; set; }
        public string? TipoReferencia { get; set; }
        public decimal Multiplicador { get; set; }
    }
}
