using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class historial_acceso
{
    public int id_historial { get; set; }

    public int id_usuario { get; set; }

    public DateTime fecha_acceso { get; set; }

    public string tipo_evento { get; set; } = null!;

    public string ip { get; set; } = null!;

    public string? user_agent { get; set; }

    public string? localizacion_geografica { get; set; }

    public bool dispositivo_conocido { get; set; }

    public string? metodo_autenticacion { get; set; }

    public string? detalles { get; set; }

    public virtual Usuario id_usuarioNavigation { get; set; } = null!;
}
