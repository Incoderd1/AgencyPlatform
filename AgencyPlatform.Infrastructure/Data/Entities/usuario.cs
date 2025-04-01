using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class usuario
{
    public int id_usuario { get; set; }

    public Guid uuid { get; set; }

    public string email { get; set; } = null!;

    public string contrasena { get; set; } = null!;

    public string? salt { get; set; }

    public string metodo_auth { get; set; } = null!;

    public string? auth_id { get; set; }

    public bool factor_2fa { get; set; }

    public string? secreto_2fa { get; set; }

    public string? permisos { get; set; }

    public DateTime fecha_registro { get; set; }

    public DateTime? ultimo_login { get; set; }

    public string estado { get; set; } = null!;

    public string? motivo_suspension { get; set; }

    public DateTime? fecha_suspension { get; set; }

    public bool verificado_email { get; set; }

    public string? token_verificacion { get; set; }

    public DateTime? fecha_expiracion_token { get; set; }

    public string? ip_registro { get; set; }

    public string? ultimo_ip { get; set; }

    public short intentos_fallidos { get; set; }

    public bool bloqueo_temporal { get; set; }

    public DateTime? fecha_bloqueo { get; set; }

    public bool cambio_contraseña_requerido { get; set; }

    public DateTime? fecha_ultimo_cambio_contraseña { get; set; }

    public string tipo_usuario { get; set; } = null!;

    public DateTime fecha_actualizacion { get; set; }

    public DateTime? ultimo_terminos_aceptados { get; set; }

    public bool acepto_marketing { get; set; }

    public string? datos_eliminacion { get; set; }

    public DateTime? ultima_actividad { get; set; }

    public virtual ICollection<acciones_antifrauide> acciones_antifrauides { get; set; } = new List<acciones_antifrauide>();

    public virtual ICollection<agencia> agencia { get; set; } = new List<agencia>();

    public virtual ICollection<cliente> clientes { get; set; } = new List<cliente>();

    public virtual ICollection<configuracion_sistema> configuracion_sistemas { get; set; } = new List<configuracion_sistema>();

    public virtual ICollection<dispositivos_usuario> dispositivos_usuarios { get; set; } = new List<dispositivos_usuario>();

    public virtual ICollection<historial_acceso_actual> historial_acceso_actuals { get; set; } = new List<historial_acceso_actual>();

    public virtual ICollection<historial_acceso_archivo> historial_acceso_archivos { get; set; } = new List<historial_acceso_archivo>();

    public virtual ICollection<historial_acceso_pasado> historial_acceso_pasados { get; set; } = new List<historial_acceso_pasado>();

    public virtual ICollection<historial_acceso> historial_accesos { get; set; } = new List<historial_acceso>();

    public virtual ICollection<historial_configuracion> historial_configuracions { get; set; } = new List<historial_configuracion>();

    public virtual ICollection<historial_verificacione> historial_verificaciones { get; set; } = new List<historial_verificacione>();

    public virtual ICollection<imagenes_perfil> imagenes_perfilrevisada_porNavigations { get; set; } = new List<imagenes_perfil>();

    public virtual ICollection<imagenes_perfil> imagenes_perfilsubida_porNavigations { get; set; } = new List<imagenes_perfil>();

    public virtual ICollection<logs_sistema_actual> logs_sistema_actuals { get; set; } = new List<logs_sistema_actual>();

    public virtual ICollection<logs_sistema_antiguo> logs_sistema_antiguos { get; set; } = new List<logs_sistema_antiguo>();

    public virtual ICollection<logs_sistema_archivo> logs_sistema_archivos { get; set; } = new List<logs_sistema_archivo>();

    public virtual ICollection<logs_sistema_reciente> logs_sistema_recientes { get; set; } = new List<logs_sistema_reciente>();

    public virtual ICollection<logs_sistema> logs_sistemas { get; set; } = new List<logs_sistema>();

    public virtual ICollection<perfile> perfiles { get; set; } = new List<perfile>();

    public virtual ICollection<verificacione> verificaciones { get; set; } = new List<verificacione>();
}
