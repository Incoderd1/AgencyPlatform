using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class VisitasPerfilActual
{
    public int IdVisita { get; set; }

    public int IdPerfil { get; set; }

    public int? IdCliente { get; set; }

    public DateTime FechaVisita { get; set; }

    public string IpVisitante { get; set; } = null!;

    public string? UserAgent { get; set; }

    public int? TiempoVisita { get; set; }

    public string? Dispositivo { get; set; }

    public string? Origen { get; set; }

    public string? RegionGeografica { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;
}
