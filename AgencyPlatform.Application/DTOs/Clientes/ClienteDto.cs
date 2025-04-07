using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Clientes
{
    public class ClienteDto
    {
        public long IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public bool EsVip { get; set; }
        public short NivelVip { get; set; }
        public DateOnly? FechaInicioVip { get; set; }
        public DateOnly? FechaFinVip { get; set; }
        public int PuntosAcumulados { get; set; }
        public int PuntosGastados { get; set; }
        public int PuntosCaducados { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string? Genero { get; set; }
        public object? Preferencias { get; set; }
        public object? Intereses { get; set; }
        public DateTime? UltimaActividad { get; set; }
        public int NumLogins { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string? OrigenRegistro { get; set; }
        public string? UbicacionHabitual { get; set; }
        public int FidelidadScore { get; set; }
    }

}
