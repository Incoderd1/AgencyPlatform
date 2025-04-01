using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; }
        public string Email { get; set; } = default!;
        public string TipoUsuario { get; set; } = default!;
    }
}
