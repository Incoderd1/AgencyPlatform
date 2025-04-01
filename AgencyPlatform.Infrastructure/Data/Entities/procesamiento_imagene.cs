using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class procesamiento_imagene
{
    public int id_procesamiento { get; set; }

    public int id_imagen { get; set; }

    public string tipo_procesamiento { get; set; } = null!;

    public string parametros_procesamiento { get; set; } = null!;

    public string url_resultado { get; set; } = null!;

    public DateTime fecha_procesamiento { get; set; }

    public string procesado_por { get; set; } = null!;

    public virtual imagenes_perfil id_imagenNavigation { get; set; } = null!;
}
