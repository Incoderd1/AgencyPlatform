using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class cliente
{
    public int id_cliente { get; set; }

    public int id_usuario { get; set; }

    public string? nombre { get; set; }

    public string? telefono { get; set; }

    public bool es_vip { get; set; }

    public short nivel_vip { get; set; }

    public DateOnly? fecha_inicio_vip { get; set; }

    public DateOnly? fecha_fin_vip { get; set; }

    public int puntos_acumulados { get; set; }

    public int puntos_gastados { get; set; }

    public int puntos_caducados { get; set; }

    public DateOnly? fecha_nacimiento { get; set; }

    public string? genero { get; set; }

    public string? preferencias { get; set; }

    public string? intereses { get; set; }

    public DateTime? ultima_actividad { get; set; }

    public int num_logins { get; set; }

    public DateTime fecha_registro { get; set; }

    public DateTime fecha_actualizacion { get; set; }

    public string? origen_registro { get; set; }

    public string? ubicacion_habitual { get; set; }

    public int fidelidad_score { get; set; }

    public int? edad { get; set; }

    public virtual ICollection<contactos_perfil> contactos_perfils { get; set; } = new List<contactos_perfil>();

    public virtual ICollection<feedback_interno> feedback_internos { get; set; } = new List<feedback_interno>();

    public virtual usuario id_usuarioNavigation { get; set; } = null!;

    public virtual ICollection<punto> puntos { get; set; } = new List<punto>();

    public virtual ICollection<suscripciones_vip> suscripciones_vips { get; set; } = new List<suscripciones_vip>();

    public virtual ICollection<visitas_perfil_actual> visitas_perfil_actuals { get; set; } = new List<visitas_perfil_actual>();

    public virtual ICollection<visitas_perfil_antiguo> visitas_perfil_antiguos { get; set; } = new List<visitas_perfil_antiguo>();

    public virtual ICollection<visitas_perfil> visitas_perfils { get; set; } = new List<visitas_perfil>();
}
