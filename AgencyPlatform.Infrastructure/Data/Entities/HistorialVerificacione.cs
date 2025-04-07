using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class HistorialVerificacione
{
    public int IdHistorial { get; set; }

    public int IdVerificacion { get; set; }

    public string? EstadoAnterior { get; set; }

    public string EstadoNuevo { get; set; } = null!;

    public DateTime FechaCambio { get; set; }

    public int? RealizadoPor { get; set; }

    public string? Comentario { get; set; }

    public int? TiempoEnEstado { get; set; }

    public string? DatosAdicionales { get; set; }

    public virtual Verificacione IdVerificacionNavigation { get; set; } = null!;

    public virtual Usuario? RealizadoPorNavigation { get; set; }
}
