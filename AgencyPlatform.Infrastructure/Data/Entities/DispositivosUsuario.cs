using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class DispositivosUsuario
{
    public int IdDispositivo { get; set; }

    public int IdUsuario { get; set; }

    public string TokenDispositivo { get; set; } = null!;

    public string? NombreDispositivo { get; set; }

    public string TipoDispositivo { get; set; } = null!;

    public string? SistemaOperativo { get; set; }

    public string? Navegador { get; set; }

    public string? FcmToken { get; set; }

    public DateTime UltimaActividad { get; set; }

    public string? IpUltimaActividad { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
