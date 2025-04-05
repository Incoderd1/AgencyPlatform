using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Perfil
{
    public class CrearPerfilDto
    {
        public int IdUsuario { get; set; }  // Aquí agregamos la propiedad IdUsuario
        public int? IdAgencia { get; set; }
        public string NombrePerfil { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public short? Edad { get; set; }
        public short? Altura { get; set; }
        public short? Peso { get; set; }
        public string? Medidas { get; set; }
        public string? ColorOjos { get; set; }
        public string? ColorCabello { get; set; }
        public string? Nacionalidad { get; set; }

        // JSONB como objetos tipados
        public List<IdiomaDto>? Idiomas { get; set; }
        public string? Descripcion { get; set; }
        public List<ServicioDto>? Servicios { get; set; }
        public List<TarifaDto>? Tarifas { get; set; }
        public List<HorarioDto>? Disponibilidad { get; set; }

        public string? UbicacionCiudad { get; set; }
        public string? UbicacionZona { get; set; }

        public string? DisponiblePara { get; set; } = "todos";
        public bool? Disponible24h { get; set; }
        public bool? DisponeLocal { get; set; }
        public bool? HaceSalidas { get; set; }
        public bool? EsIndependiente { get; set; }

        public string? TelefonoContacto { get; set; }
        public string? Whatsapp { get; set; }
        public string? EmailContacto { get; set; }
    }
}
