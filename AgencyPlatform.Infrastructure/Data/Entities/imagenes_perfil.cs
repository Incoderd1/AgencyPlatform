using System;
using System.Collections.Generic;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class imagenes_perfil
{
    public int id_imagen { get; set; }

    public int id_perfil { get; set; }

    public Guid uuid_imagen { get; set; }

    public string url_imagen { get; set; } = null!;

    public string url_thumbnail { get; set; } = null!;

    public string? url_media { get; set; }

    public string? url_webp { get; set; }

    public string? hash_imagen { get; set; }

    public bool es_principal { get; set; }

    public short orden { get; set; }

    public int ancho { get; set; }

    public int alto { get; set; }

    public int tamaño_bytes { get; set; }

    public string tipo_mime { get; set; } = null!;

    public string? descripcion { get; set; }

    public string? tags { get; set; }

    public string? metadata_exif { get; set; }

    public bool contenido_sensible { get; set; }

    public short nivel_blurring { get; set; }

    public DateTime fecha_subida { get; set; }

    public int? subida_por { get; set; }

    public string estado { get; set; } = null!;

    public string? motivo_rechazo { get; set; }

    public int? revisada_por { get; set; }

    public DateTime? fecha_revision { get; set; }

    public virtual perfile id_perfilNavigation { get; set; } = null!;

    public virtual ICollection<procesamiento_imagene> procesamiento_imagenes { get; set; } = new List<procesamiento_imagene>();

    public virtual usuario? revisada_porNavigation { get; set; }

    public virtual usuario? subida_porNavigation { get; set; }
}
