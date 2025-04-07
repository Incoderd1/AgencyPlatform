using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Cupones
{
    public class CrearCuponDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string TipoDescuento { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal? MinimoCompra { get; set; }
        public decimal? MaximoDescuento { get; set; }
        public int? UsosMaximos { get; set; }
        public int UsosPorCliente { get; set; } = 1;
        public string AplicaA { get; set; }
        public int? RequierePuntos { get; set; }
    }

}
