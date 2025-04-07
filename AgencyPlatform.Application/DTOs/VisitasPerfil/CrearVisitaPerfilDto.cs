﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.VisitasPerfil
{
    public class CrearVisitaPerfilDto
    {
        public int IdPerfil { get; set; }
        public long? IdCliente { get; set; }
        public DateTime FechaVisita { get; set; }
        public int? TiempoVisita { get; set; }
        public string? Dispositivo { get; set; }
        public string? Origen { get; set; }
        public string? RegionGeografica { get; set; }
    }
}
