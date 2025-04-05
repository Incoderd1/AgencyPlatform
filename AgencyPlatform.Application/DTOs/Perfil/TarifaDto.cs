using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Perfil
{
    public class TarifaDto
    {
        public string Tipo { get; set; } = null!;
        public decimal Precio { get; set; }
    }
}
