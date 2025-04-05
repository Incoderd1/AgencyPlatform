using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.PerfilImagen
{
    public class UpdateImagenPerfilDto
    {
        public string? Descripcion { get; set; }
        public object? Tags { get; set; }
        public object? MetadataExif { get; set; }
        public bool? EsPrincipal { get; set; }
        public bool? ContenidoSensible { get; set; }
        public short? NivelBlurring { get; set; }
    }
}
