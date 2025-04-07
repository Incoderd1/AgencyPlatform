using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class Agencia
{
    public int IdAgencia { get; set; }

    public int IdUsuario { get; set; }

    public string NombreComercial { get; set; } = null!;

    public string? RazonSocial { get; set; }

    public string? NifCif { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Region { get; set; }

    public string? Pais { get; set; }

    public string? CodigoPostal { get; set; }

    public string Telefono { get; set; } = null!;

    public string EmailContacto { get; set; } = null!;

    public string? SitioWeb { get; set; }

    public string? Descripcion { get; set; }

    public string? Horario { get; set; }

    public string? LogoUrl { get; set; }

    public bool Verificada { get; set; }

    public DateTime? FechaVerificacion { get; set; }

    public string? DocumentoVerificacion { get; set; }

    public int NumPerfilesActivos { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Perfile> PerfileIdAgenciaNavigations { get; set; } = new List<Perfile>();

    public virtual ICollection<Perfile> PerfileQuienVerificoNavigations { get; set; } = new List<Perfile>();
}
