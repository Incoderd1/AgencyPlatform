using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class feedback_interno
{
    public int id_feedback { get; set; }

    public int id_cliente { get; set; }

    public int id_perfil { get; set; }

    public short puntuacion { get; set; }

    public string? comentario { get; set; }

    public DateOnly? fecha_experiencia { get; set; }

    public DateTime fecha_registro { get; set; }

    public bool verificado { get; set; }

    public string visibilidad { get; set; } = null!;

    public string estado { get; set; } = null!;

    public virtual cliente id_clienteNavigation { get; set; } = null!;

    public virtual perfile id_perfilNavigation { get; set; } = null!;
}
