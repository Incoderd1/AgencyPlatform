using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class historial_verificacione
{
    public int id_historial { get; set; }

    public int id_verificacion { get; set; }

    public string? estado_anterior { get; set; }

    public string estado_nuevo { get; set; } = null!;

    public DateTime fecha_cambio { get; set; }

    public int? realizado_por { get; set; }

    public string? comentario { get; set; }

    public int? tiempo_en_estado { get; set; }

    public string? datos_adicionales { get; set; }

    public virtual Verificacione id_verificacionNavigation { get; set; } = null!;

    public virtual Usuario? realizado_porNavigation { get; set; }
}
