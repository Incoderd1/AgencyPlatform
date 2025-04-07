using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class Cliente
{
    public long IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public bool EsVip { get; set; }

    public short NivelVip { get; set; }

    public DateOnly? FechaInicioVip { get; set; }

    public DateOnly? FechaFinVip { get; set; }

    public int PuntosAcumulados { get; set; }

    public int PuntosGastados { get; set; }

    public int PuntosCaducados { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Genero { get; set; }

    public string? Preferencias { get; set; }

    public string? Intereses { get; set; }

    public DateTime? UltimaActividad { get; set; }

    public int NumLogins { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public string? OrigenRegistro { get; set; }

    public string? UbicacionHabitual { get; set; }

    public int FidelidadScore { get; set; }

    public int? Edad { get; set; }

    public virtual ICollection<ContactosPerfil> ContactosPerfils { get; set; } = new List<ContactosPerfil>();

    public virtual ICollection<CuponesCliente> CuponesClientes { get; set; } = new List<CuponesCliente>();

    public virtual ICollection<FeedbackInterno> FeedbackInternos { get; set; } = new List<FeedbackInterno>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Punto> Puntos { get; set; } = new List<Punto>();

    public virtual ICollection<SuscripcionesVip> SuscripcionesVips { get; set; } = new List<SuscripcionesVip>();

    public virtual ICollection<VisitasPerfilActual> VisitasPerfilActuals { get; set; } = new List<VisitasPerfilActual>();

    public virtual ICollection<VisitasPerfilAntiguo> VisitasPerfilAntiguos { get; set; } = new List<VisitasPerfilAntiguo>();

    public virtual ICollection<VisitasPerfil> VisitasPerfils { get; set; } = new List<VisitasPerfil>();
}
