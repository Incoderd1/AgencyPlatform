using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.PerfilImagen
{
    public class CrearImagenPerfilDto
    {
        //public int IdPerfil { get; set; }
        public string UrlImagen { get; set; } = null!;
        public string UrlThumbnail { get; set; } = null!;
        public string? UrlMedia { get; set; }
        public string? UrlWebp { get; set; }
        public string TipoMime { get; set; } = null!;
        public int Ancho { get; set; }
        public int Alto { get; set; }
        public int TamañoBytes { get; set; }
        public string? Descripcion { get; set; }
        public object? Tags { get; set; }
        public object? MetadataExif { get; set; }
        public bool? EsPrincipal { get; set; } = false;
        public bool? ContenidoSensible { get; set; } = false;
        public short? NivelBlurring { get; set; } = 0;
    }
}

