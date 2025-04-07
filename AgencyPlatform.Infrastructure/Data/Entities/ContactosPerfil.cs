using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class ContactosPerfil
{
    public int IdContacto { get; set; }

    public int IdPerfil { get; set; }

    public long? IdCliente { get; set; }

    public string TipoContacto { get; set; } = null!;

    public DateTime FechaContacto { get; set; }

    public string IpContacto { get; set; } = null!;

    public string? Mensaje { get; set; }

    public string? TelefonoContacto { get; set; }

    public string? EmailContacto { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaRespuesta { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;
}
