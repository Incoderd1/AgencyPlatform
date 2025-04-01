using System.ComponentModel.DataAnnotations;

namespace AgencyPlatform.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string TipoUsuario { get; set; } = "cliente"; // default para usuarios normales
    }
}
