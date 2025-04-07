using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.CuponesCliente
{
    public class ActualizarCuponClienteDto
    {
        public DateTime? FechaUso { get; set; }
        public string Estado { get; set; }
        public int? IdTransaccion { get; set; }
        public int? PuntosCanjeados { get; set; }
    }
}
