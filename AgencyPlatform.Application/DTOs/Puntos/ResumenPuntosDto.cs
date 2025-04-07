using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Puntos
{
    public class ResumenPuntosDto
    {
        public int TotalAcumulado { get; set; }
        public int TotalUsado { get; set; }
        public int TotalExpirado { get; set; }
        public int TotalDisponible => TotalAcumulado - TotalUsado - TotalExpirado;
    }
}
