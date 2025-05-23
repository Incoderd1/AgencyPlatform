﻿using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class HistorialAccesoActual
{
    public int IdHistorial { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaAcceso { get; set; }

    public string TipoEvento { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public string? UserAgent { get; set; }

    public string? LocalizacionGeografica { get; set; }

    public bool DispositivoConocido { get; set; }

    public string? MetodoAutenticacion { get; set; }

    public string? Detalles { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
