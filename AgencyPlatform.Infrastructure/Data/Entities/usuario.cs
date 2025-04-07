using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public Guid Uuid { get; set; }

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? Salt { get; set; }

    public string MetodoAuth { get; set; } = null!;

    public string? AuthId { get; set; }

    public bool Factor2fa { get; set; }

    public string? Secreto2fa { get; set; }

    public string? Permisos { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public string Estado { get; set; } = null!;

    public string? MotivoSuspension { get; set; }

    public DateTime? FechaSuspension { get; set; }

    public bool VerificadoEmail { get; set; }

    public string? TokenVerificacion { get; set; }

    public DateTime? FechaExpiracionToken { get; set; }

    public string? IpRegistro { get; set; }

    public string? UltimoIp { get; set; }

    public short IntentosFallidos { get; set; }

    public bool BloqueoTemporal { get; set; }

    public DateTime? FechaBloqueo { get; set; }

    public bool CambioContraseñaRequerido { get; set; }

    public DateTime? FechaUltimoCambioContraseña { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public DateTime FechaActualizacion { get; set; }

    public DateTime? UltimoTerminosAceptados { get; set; }

    public bool AceptoMarketing { get; set; }

    public string? DatosEliminacion { get; set; }

    public DateTime? UltimaActividad { get; set; }

    public virtual ICollection<AccionesAntifrauide> AccionesAntifrauides { get; set; } = new List<AccionesAntifrauide>();

    public virtual ICollection<Agencia> Agencia { get; set; } = new List<Agencia>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<ConfiguracionSistema> ConfiguracionSistemas { get; set; } = new List<ConfiguracionSistema>();

    public virtual ICollection<DispositivosUsuario> DispositivosUsuarios { get; set; } = new List<DispositivosUsuario>();

    public virtual ICollection<HistorialAccesoActual> HistorialAccesoActuals { get; set; } = new List<HistorialAccesoActual>();

    public virtual ICollection<HistorialAccesoArchivo> HistorialAccesoArchivos { get; set; } = new List<HistorialAccesoArchivo>();

    public virtual ICollection<HistorialAccesoPasado> HistorialAccesoPasados { get; set; } = new List<HistorialAccesoPasado>();

    public virtual ICollection<HistorialAcceso> HistorialAccesos { get; set; } = new List<HistorialAcceso>();

    public virtual ICollection<HistorialConfiguracion> HistorialConfiguracions { get; set; } = new List<HistorialConfiguracion>();

    public virtual ICollection<HistorialVerificacione> HistorialVerificaciones { get; set; } = new List<HistorialVerificacione>();

    public virtual ICollection<ImagenesPerfil> ImagenesPerfilRevisadaPorNavigations { get; set; } = new List<ImagenesPerfil>();

    public virtual ICollection<ImagenesPerfil> ImagenesPerfilSubidaPorNavigations { get; set; } = new List<ImagenesPerfil>();

    public virtual ICollection<LogsSistemaActual> LogsSistemaActuals { get; set; } = new List<LogsSistemaActual>();

    public virtual ICollection<LogsSistemaAntiguo> LogsSistemaAntiguos { get; set; } = new List<LogsSistemaAntiguo>();

    public virtual ICollection<LogsSistemaArchivo> LogsSistemaArchivos { get; set; } = new List<LogsSistemaArchivo>();

    public virtual ICollection<LogsSistemaReciente> LogsSistemaRecientes { get; set; } = new List<LogsSistemaReciente>();

    public virtual ICollection<LogsSistema> LogsSistemas { get; set; } = new List<LogsSistema>();

    public virtual ICollection<Perfile> Perfiles { get; set; } = new List<Perfile>();

    public virtual ICollection<Verificacione> Verificaciones { get; set; } = new List<Verificacione>();
}
