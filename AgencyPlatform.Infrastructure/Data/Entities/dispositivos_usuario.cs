using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class dispositivos_usuario
{
    public int id_dispositivo { get; set; }

    public int id_usuario { get; set; }

    public string token_dispositivo { get; set; } = null!;

    public string? nombre_dispositivo { get; set; }

    public string tipo_dispositivo { get; set; } = null!;

    public string? sistema_operativo { get; set; }

    public string? navegador { get; set; }

    public string? fcm_token { get; set; }

    public DateTime ultima_actividad { get; set; }

    public string? ip_ultima_actividad { get; set; }

    public string estado { get; set; } = null!;

    public DateTime fecha_registro { get; set; }

    public virtual usuario id_usuarioNavigation { get; set; } = null!;
}
