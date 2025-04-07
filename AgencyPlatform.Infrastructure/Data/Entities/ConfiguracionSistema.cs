using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class ConfiguracionSistema
{
    public int IdConfig { get; set; }

    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Grupo { get; set; } = null!;

    public bool Modificable { get; set; }

    public string NivelCache { get; set; } = null!;

    public int? TiempoExpiracionCache { get; set; }

    public string Entorno { get; set; } = null!;

    public DateTime FechaActualizacion { get; set; }

    public int? ActualizadoPor { get; set; }

    public int Version { get; set; }

    public virtual Usuario? ActualizadoPorNavigation { get; set; }

    public virtual ICollection<HistorialConfiguracion> HistorialConfiguracions { get; set; } = new List<HistorialConfiguracion>();
}
