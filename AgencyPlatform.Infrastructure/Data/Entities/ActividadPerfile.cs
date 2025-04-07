using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class ActividadPerfile
{
    public int IdActividad { get; set; }

    public int IdPerfil { get; set; }

    public string TipoActividad { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? IpRegistro { get; set; }

    public string? Localizacion { get; set; }

    public string? Notas { get; set; }

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;
}
