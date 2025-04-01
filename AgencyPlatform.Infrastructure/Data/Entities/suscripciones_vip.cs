using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class suscripciones_vip
{
    public int id_suscripcion { get; set; }

    public int id_cliente { get; set; }

    public int id_plan { get; set; }

    public string numero_suscripcion { get; set; } = null!;

    public DateTime fecha_inicio { get; set; }

    public DateTime fecha_fin { get; set; }

    public DateTime? fecha_renovacion { get; set; }

    public DateTime? fecha_proximo_cargo { get; set; }

    public string tipo_ciclo { get; set; } = null!;

    public decimal monto_base { get; set; }

    public decimal monto_descuento { get; set; }

    public decimal impuestos { get; set; }

    public decimal monto_pagado { get; set; }

    public string moneda { get; set; } = null!;

    public bool auto_renovacion { get; set; }

    public int intentos_cobro { get; set; }

    public DateTime? fecha_ultimo_intento { get; set; }

    public string estado { get; set; } = null!;

    public string origen_suscripcion { get; set; } = null!;

    public string? cupon_aplicado { get; set; }

    public string metodo_pago { get; set; } = null!;

    public string gateway_pago { get; set; } = null!;

    public string? id_cliente_gateway { get; set; }

    public string? id_suscripcion_gateway { get; set; }

    public string? datos_pago { get; set; }

    public string? referencia_pago { get; set; }

    public string? id_transaccion_pago { get; set; }

    public DateTime? fecha_cancelacion { get; set; }

    public DateTime? efectiva_hasta { get; set; }

    public string? motivo_cancelacion { get; set; }

    public string solicitada_por { get; set; } = null!;

    public string? notas_internas { get; set; }

    public bool ha_recibido_recordatorio { get; set; }

    public virtual cliente id_clienteNavigation { get; set; } = null!;

    public virtual membresias_vip id_planNavigation { get; set; } = null!;
}
