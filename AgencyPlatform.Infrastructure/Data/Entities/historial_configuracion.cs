using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class historial_configuracion
{
    public int id_historial { get; set; }

    public int id_config { get; set; }

    public string? valor_anterior { get; set; }

    public string? valor_nuevo { get; set; }

    public DateTime fecha_cambio { get; set; }

    public int? realizado_por { get; set; }

    public string? motivo { get; set; }

    public string? ip_origen { get; set; }

    public virtual ConfiguracionSistema id_configNavigation { get; set; } = null!;

    public virtual Usuario? realizado_porNavigation { get; set; }
}
