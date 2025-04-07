using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Cupones
{
    public class CanjearCuponDto
    {
        public string Codigo { get; set; } = default!; // 👈 Esta línea FALTABA
        public int IdCliente { get; set; }
    }

}
