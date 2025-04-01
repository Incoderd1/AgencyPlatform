using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class contactos_perfil
{
    public int id_contacto { get; set; }

    public int id_perfil { get; set; }

    public int? id_cliente { get; set; }

    public string tipo_contacto { get; set; } = null!;

    public DateTime fecha_contacto { get; set; }

    public string ip_contacto { get; set; } = null!;

    public string? mensaje { get; set; }

    public string? telefono_contacto { get; set; }

    public string? email_contacto { get; set; }

    public string estado { get; set; } = null!;

    public DateTime? fecha_respuesta { get; set; }

    public virtual cliente? id_clienteNavigation { get; set; }

    public virtual perfile id_perfilNavigation { get; set; } = null!;
}
