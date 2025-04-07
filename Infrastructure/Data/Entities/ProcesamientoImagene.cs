using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class ProcesamientoImagene
{
    public int IdProcesamiento { get; set; }

    public int IdImagen { get; set; }

    public string TipoProcesamiento { get; set; } = null!;

    public string ParametrosProcesamiento { get; set; } = null!;

    public string UrlResultado { get; set; } = null!;

    public DateTime FechaProcesamiento { get; set; }

    public string ProcesadoPor { get; set; } = null!;

    public virtual ImagenesPerfil IdImagenNavigation { get; set; } = null!;
}
