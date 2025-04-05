using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Perfil
{
    public class UpdatePerfilDto : CrearPerfilDto
    {
        public bool? Verificado { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public int? QuienVerifico { get; set; }
        public bool? Destacado { get; set; }
        public DateOnly? FechaInicioDestacado { get; set; }
        public DateOnly? FechaFinDestacado { get; set; }
        public string? Estado { get; set; }

        // JSONB sobrescritos como objetos para override en esta clase
        public new List<IdiomaDto>? Idiomas { get; set; }
        public new List<ServicioDto>? Servicios { get; set; }
        public new List<TarifaDto>? Tarifas { get; set; }
        public new List<HorarioDto>? Disponibilidad { get; set; }
    }

}