using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class Perfile
{
    public int IdPerfil { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdAgencia { get; set; }

    public string NombrePerfil { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public short Edad { get; set; }

    public short? Altura { get; set; }

    public short? Peso { get; set; }

    public string? Medidas { get; set; }

    public string? ColorOjos { get; set; }

    public string? ColorCabello { get; set; }

    public string? Nacionalidad { get; set; }

    public string? Idiomas { get; set; }

    public string? Descripcion { get; set; }

    public string? Servicios { get; set; }

    public string? Tarifas { get; set; }

    public string? UbicacionCiudad { get; set; }

    public string? UbicacionZona { get; set; }

    public string? Disponibilidad { get; set; }

    public string DisponiblePara { get; set; } = null!;

    public bool Disponible24h { get; set; }

    public bool DisponeLocal { get; set; }

    public bool HaceSalidas { get; set; }

    public bool Verificado { get; set; }

    public DateTime? FechaVerificacion { get; set; }

    public int? QuienVerifico { get; set; }

    public bool EsIndependiente { get; set; }

    public string? TelefonoContacto { get; set; }

    public string? Whatsapp { get; set; }

    public string? EmailContacto { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public string Estado { get; set; } = null!;

    public bool Destacado { get; set; }

    public DateOnly? FechaInicioDestacado { get; set; }

    public DateOnly? FechaFinDestacado { get; set; }

    public DateTime? UltimoOnline { get; set; }

    public long NumVisitas { get; set; }

    public long NumContactos { get; set; }

    public decimal? PuntuacionInterna { get; set; }

    public string NivelPopularidad { get; set; } = null!;

    public virtual ICollection<ActividadPerfile> ActividadPerfiles { get; set; } = new List<ActividadPerfile>();

    public virtual ICollection<ContactosPerfil> ContactosPerfils { get; set; } = new List<ContactosPerfil>();

    public virtual ICollection<FeedbackInterno> FeedbackInternos { get; set; } = new List<FeedbackInterno>();

    public virtual Agencia? IdAgenciaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<ImagenesPerfil> ImagenesPerfils { get; set; } = new List<ImagenesPerfil>();

    public virtual ICollection<MetricasPerfil> MetricasPerfils { get; set; } = new List<MetricasPerfil>();

    public virtual Agencia? QuienVerificoNavigation { get; set; }

    public virtual ICollection<ResumenHistoricoVisita> ResumenHistoricoVisita { get; set; } = new List<ResumenHistoricoVisita>();

    public virtual ICollection<VisitasPerfilActual> VisitasPerfilActuals { get; set; } = new List<VisitasPerfilActual>();

    public virtual ICollection<VisitasPerfilAntiguo> VisitasPerfilAntiguos { get; set; } = new List<VisitasPerfilAntiguo>();

    public virtual ICollection<VisitasPerfil> VisitasPerfils { get; set; } = new List<VisitasPerfil>();
}
