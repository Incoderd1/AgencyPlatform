using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.MembresiasVip
{
    public class MembresiaVipDto
    {
        public int IdPlan { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal PrecioMensual { get; set; }
        public decimal? PrecioTrimestral { get; set; }
        public decimal? PrecioAnual { get; set; }
        public BeneficiosVipModel Beneficios { get; set; } = null!;
        public int PuntosMensuales { get; set; }
        public short ReduccionAnuncios { get; set; }
        public short DescuentosAdicionales { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
