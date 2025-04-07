using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Clientes
{
    public class ClienteResumenDto
    {
        public long IdCliente { get; set; }
        public string? Nombre { get; set; }
        public bool EsVip { get; set; }
        public short NivelVip { get; set; }
        public int? Edad { get; set; }
        public int PuntosAcumulados { get; set; }          // <--- NUEVO
        public int Fidelidad { get; set; }                  // <--- NUEVO
        public DateTime? UltimaActividad { get; set; }
    }

}
