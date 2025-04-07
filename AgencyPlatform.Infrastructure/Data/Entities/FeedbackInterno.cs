using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class FeedbackInterno
{
    public int IdFeedback { get; set; }

    public long IdCliente { get; set; }

    public int IdPerfil { get; set; }

    public short Puntuacion { get; set; }

    public string? Comentario { get; set; }

    public DateOnly? FechaExperiencia { get; set; }

    public DateTime FechaRegistro { get; set; }

    public bool Verificado { get; set; }

    public string Visibilidad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;
}
