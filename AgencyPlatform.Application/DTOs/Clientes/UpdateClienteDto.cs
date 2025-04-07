using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Clientes
{
    public class UpdateClienteDto
    {
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public bool? EsVip { get; set; }
        public short? NivelVip { get; set; }
        public DateOnly? FechaInicioVip { get; set; }
        public DateOnly? FechaFinVip { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public object? Preferencias { get; set; }
        public object? Intereses { get; set; }
        public string? OrigenRegistro { get; set; }
        public string? UbicacionHabitual { get; set; }
    }

}
