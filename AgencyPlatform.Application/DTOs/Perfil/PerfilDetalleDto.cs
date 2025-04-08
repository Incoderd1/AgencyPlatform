using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Perfil
{
    public class PerfilDetalleDto
    {
        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? TelefonoContacto { get; set; }
        public string? Whatsapp { get; set; }
        public List<string> GaleriaImagenes { get; set; } = new();
    }

}
