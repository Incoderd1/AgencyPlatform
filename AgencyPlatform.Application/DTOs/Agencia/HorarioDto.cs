using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Agencia
{
    public class HorarioDto
    {

        public string Dias { get; set; } = null!;
        public string HoraInicio { get; set; } = null!;
        public string HoraFin { get; set; } = null!;

    }
}
