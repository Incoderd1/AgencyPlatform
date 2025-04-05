using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Perfil
{
    public class HorarioDto
    {
        public string Dia { get; set; } = null!;
        public List<string> Horas { get; set; } = new();
    }
}
