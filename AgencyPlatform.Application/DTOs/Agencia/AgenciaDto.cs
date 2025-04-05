using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Agencia
{
    public class AgenciaDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreComercial { get; set; } = null!;
        public string? RazonSocial { get; set; }
        public string? NifCif { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Region { get; set; }
        public string? Pais { get; set; }
        public string? CodigoPostal { get; set; }
        public string Telefono { get; set; } = null!;
        public string EmailContacto { get; set; } = null!;
        public string? SitioWeb { get; set; }
        public string? Descripcion { get; set; }

        // ✅ Aquí cambiamos string por HorarioDto
        public HorarioDto? Horario { get; set; }

        public string? LogoUrl { get; set; }
        public bool Verificada { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public string? DocumentoVerificacion { get; set; }
        public int NumPerfilesActivos { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Estado { get; set; } = null!;
    }

}
