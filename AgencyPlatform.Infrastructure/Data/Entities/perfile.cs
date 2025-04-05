using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class perfile
{
    public int id_perfil { get; set; }

    public int? id_usuario { get; set; }

    public int? id_agencia { get; set; }

    public string nombre_perfil { get; set; } = null!;

    public string genero { get; set; } = null!;

    public short edad { get; set; }

    public short? altura { get; set; }

    public short? peso { get; set; }

    public string? medidas { get; set; }

    public string? color_ojos { get; set; }

    public string? color_cabello { get; set; }

    public string? nacionalidad { get; set; }

    public string? idiomas { get; set; }

    public string? descripcion { get; set; }

    public string? servicios { get; set; }

    public string? tarifas { get; set; }

    public string? ubicacion_ciudad { get; set; }

    public string? ubicacion_zona { get; set; }

    public string? disponibilidad { get; set; }

    public string disponible_para { get; set; } = null!;

    public bool disponible_24h { get; set; }

    public bool dispone_local { get; set; }

    public bool hace_salidas { get; set; }

    public bool verificado { get; set; }

    public DateTime? fecha_verificacion { get; set; }

    public int? quien_verifico { get; set; }

    public bool es_independiente { get; set; }

    public string? telefono_contacto { get; set; }

    public string? whatsapp { get; set; }

    public string? email_contacto { get; set; }

    public DateTime fecha_registro { get; set; }

    public DateTime fecha_actualizacion { get; set; }

    public string estado { get; set; } = null!;

    public bool destacado { get; set; }

    public DateOnly? fecha_inicio_destacado { get; set; }

    public DateOnly? fecha_fin_destacado { get; set; }

    public DateTime? ultimo_online { get; set; }

    public long num_visitas { get; set; }

    public long num_contactos { get; set; }

    public decimal? puntuacion_interna { get; set; }

    public string nivel_popularidad { get; set; } = null!;

    public virtual ICollection<actividad_perfile> actividad_perfiles { get; set; } = new List<actividad_perfile>();

    public virtual ICollection<contactos_perfil> contactos_perfils { get; set; } = new List<contactos_perfil>();

    public virtual ICollection<feedback_interno> feedback_internos { get; set; } = new List<feedback_interno>();

    public virtual agencia? id_agencia_navigation { get; set; }

    public virtual usuario? id_usuario_navigation { get; set; }

    public virtual ICollection<imagenes_perfil> imagenes_perfils { get; set; } = new List<imagenes_perfil>();

    public virtual ICollection<metricas_perfil> metricas_perfils { get; set; } = new List<metricas_perfil>();

    public virtual agencia? quien_verifico_navigation { get; set; }

    public virtual ICollection<resumen_historico_visita> resumen_historico_visita { get; set; } = new List<resumen_historico_visita>();

    public virtual ICollection<visitas_perfil_actual> visitas_perfil_actuals { get; set; } = new List<visitas_perfil_actual>();

    public virtual ICollection<visitas_perfil_antiguo> visitas_perfil_antiguos { get; set; } = new List<visitas_perfil_antiguo>();

    public virtual ICollection<visitas_perfil> visitas_perfils { get; set; } = new List<visitas_perfil>();
}
