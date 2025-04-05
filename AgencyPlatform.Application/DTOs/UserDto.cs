using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public string Estado { get; set; }
        public bool VerificadoEmail { get; set; }
        public bool Factor2FA { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoLogin { get; set; }
    }
}
