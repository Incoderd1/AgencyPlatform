using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class verificacione
{
    public int id_verificacion { get; set; }

    public string codigo_verificacion { get; set; } = null!;

    public string tipo_entidad { get; set; } = null!;

    public int id_entidad { get; set; }

    public string estado { get; set; } = null!;

    public short nivel_verificacion { get; set; }

    public string? documentos_url { get; set; }

    public string? documentos_hash { get; set; }

    public DateOnly? fecha_caducidad_documentos { get; set; }

    public string? notas_admin { get; set; }

    public string? notas_internas { get; set; }

    public string? checklist_verificacion { get; set; }

    public string? motivo_rechazo { get; set; }

    public string? sugerencias_correccion { get; set; }

    public int? verificado_por { get; set; }

    public string? historial_estados { get; set; }

    public decimal? puntuacion_riesgo { get; set; }

    public DateTime fecha_solicitud { get; set; }

    public DateTime? fecha_asignacion { get; set; }

    public DateTime? fecha_ultima_actualizacion { get; set; }

    public DateTime? fecha_verificacion { get; set; }

    public int? tiempo_proceso { get; set; }

    public DateTime? valido_desde { get; set; }

    public DateTime? valido_hasta { get; set; }

    public bool renovacion_automatica { get; set; }

    public bool recordatorio_enviado { get; set; }

    public bool pago_recibido { get; set; }

    public decimal? monto_pagado { get; set; }

    public string moneda { get; set; } = null!;

    public int? id_transaccion { get; set; }

    public string? metodo_pago { get; set; }

    public string prioridad { get; set; } = null!;

    public string? origen_solicitud { get; set; }

    public virtual ICollection<historial_verificacione> historial_verificaciones { get; set; } = new List<historial_verificacione>();

    public virtual usuario? verificado_porNavigation { get; set; }
}
