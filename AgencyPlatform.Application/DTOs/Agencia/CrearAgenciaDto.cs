using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Agencia
{
    public class CrearAgenciaDto
    {
        //public int IdUsuario { get; set; }
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

        // Ahora Horario es un objeto
        public HorarioDto? Horario { get; set; }

        public string? LogoUrl { get; set; }
    }

}
