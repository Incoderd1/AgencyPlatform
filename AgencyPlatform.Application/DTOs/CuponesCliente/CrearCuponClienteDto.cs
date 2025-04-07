using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.CuponesCliente
{
    public class CrearCuponClienteDto
    {
        public int IdCupon { get; set; }
        public int IdCliente { get; set; }
        public int? IdTransaccion { get; set; }
        public int? PuntosCanjeados { get; set; }
    }
}
