using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class LogsSistemaAntiguo
{
    public int IdLog { get; set; }

    public string Tipo { get; set; } = null!;

    public short NivelSeveridad { get; set; }

    public string Modulo { get; set; } = null!;

    public string? Submodulo { get; set; }

    public string Mensaje { get; set; } = null!;

    public string? CodigoError { get; set; }

    public string? Datos { get; set; }

    public int? IdUsuario { get; set; }

    public string? Ip { get; set; }

    public string? UserAgent { get; set; }

    public int? TiempoProcesamiento { get; set; }

    public int? MemoriaUtilizada { get; set; }

    public DateTime FechaLog { get; set; }

    public string? HashIdentificador { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
