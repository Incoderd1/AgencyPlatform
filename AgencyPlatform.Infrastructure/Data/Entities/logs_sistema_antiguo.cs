using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class logs_sistema_antiguo
{
    public int id_log { get; set; }

    public string tipo { get; set; } = null!;

    public short nivel_severidad { get; set; }

    public string modulo { get; set; } = null!;

    public string? submodulo { get; set; }

    public string mensaje { get; set; } = null!;

    public string? codigo_error { get; set; }

    public string? datos { get; set; }

    public int? id_usuario { get; set; }

    public string? ip { get; set; }

    public string? user_agent { get; set; }

    public int? tiempo_procesamiento { get; set; }

    public int? memoria_utilizada { get; set; }

    public DateTime fecha_log { get; set; }

    public string? hash_identificador { get; set; }

    public virtual Usuario? id_usuarioNavigation { get; set; }
}
