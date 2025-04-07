using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class Verificacione
{
    public int IdVerificacion { get; set; }

    public string CodigoVerificacion { get; set; } = null!;

    public string TipoEntidad { get; set; } = null!;

    public int IdEntidad { get; set; }

    public string Estado { get; set; } = null!;

    public short NivelVerificacion { get; set; }

    public string? DocumentosUrl { get; set; }

    public string? DocumentosHash { get; set; }

    public DateOnly? FechaCaducidadDocumentos { get; set; }

    public string? NotasAdmin { get; set; }

    public string? NotasInternas { get; set; }

    public string? ChecklistVerificacion { get; set; }

    public string? MotivoRechazo { get; set; }

    public string? SugerenciasCorreccion { get; set; }

    public int? VerificadoPor { get; set; }

    public string? HistorialEstados { get; set; }

    public decimal? PuntuacionRiesgo { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public DateTime? FechaUltimaActualizacion { get; set; }

    public DateTime? FechaVerificacion { get; set; }

    public int? TiempoProceso { get; set; }

    public DateTime? ValidoDesde { get; set; }

    public DateTime? ValidoHasta { get; set; }

    public bool RenovacionAutomatica { get; set; }

    public bool RecordatorioEnviado { get; set; }

    public bool PagoRecibido { get; set; }

    public decimal? MontoPagado { get; set; }

    public string Moneda { get; set; } = null!;

    public int? IdTransaccion { get; set; }

    public string? MetodoPago { get; set; }

    public string Prioridad { get; set; } = null!;

    public string? OrigenSolicitud { get; set; }

    public virtual ICollection<HistorialVerificacione> HistorialVerificaciones { get; set; } = new List<HistorialVerificacione>();

    public virtual Usuario? VerificadoPorNavigation { get; set; }
}
