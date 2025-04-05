using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class agencia
{
    public int id_agencia { get; set; }

    public int id_usuario { get; set; }

    public string nombre_comercial { get; set; } = null!;

    public string? razon_social { get; set; }

    public string? nif_cif { get; set; }

    public string? direccion { get; set; }

    public string? ciudad { get; set; }

    public string? region { get; set; }

    public string? pais { get; set; }

    public string? codigo_postal { get; set; }

    public string telefono { get; set; } = null!;

    public string email_contacto { get; set; } = null!;

    public string? sitio_web { get; set; }

    public string? descripcion { get; set; }

    public string? horario { get; set; }

    public string? logo_url { get; set; }

    public bool verificada { get; set; }

    public DateTime? fecha_verificacion { get; set; }

    public string? documento_verificacion { get; set; }

    public int num_perfiles_activos { get; set; }

    public DateTime fecha_registro { get; set; }

    public DateTime fecha_actualizacion { get; set; }

    public string estado { get; set; } = null!;

    public virtual usuario id_usuario_navigation { get; set; } = null!;

    public virtual ICollection<perfile> perfile_id_agencia_navigations { get; set; } = new List<perfile>();

    public virtual ICollection<perfile> perfile_quien_verifico_navigations { get; set; } = new List<perfile>();

}
