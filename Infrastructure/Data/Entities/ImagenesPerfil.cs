using System;
using System.Collections.Generic;

namespace AgencyPlatform.API;

public partial class ImagenesPerfil
{
    public int IdImagen { get; set; }

    public int IdPerfil { get; set; }

    public Guid UuidImagen { get; set; }

    public string UrlImagen { get; set; } = null!;

    public string UrlThumbnail { get; set; } = null!;

    public string? UrlMedia { get; set; }

    public string? UrlWebp { get; set; }

    public string? HashImagen { get; set; }

    public bool EsPrincipal { get; set; }

    public int Orden { get; set; }

    public int Ancho { get; set; }

    public int Alto { get; set; }

    public int TamañoBytes { get; set; }

    public string TipoMime { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Tags { get; set; }

    public string? MetadataExif { get; set; }

    public bool ContenidoSensible { get; set; }

    public short NivelBlurring { get; set; }

    public DateTime FechaSubida { get; set; }

    public int? SubidaPor { get; set; }

    public string Estado { get; set; } = null!;

    public string? MotivoRechazo { get; set; }

    public int? RevisadaPor { get; set; }

    public DateTime? FechaRevision { get; set; }

    public virtual Perfile IdPerfilNavigation { get; set; } = null!;

    public virtual ICollection<ProcesamientoImagene> ProcesamientoImagenes { get; set; } = new List<ProcesamientoImagene>();

    public virtual Usuario? RevisadaPorNavigation { get; set; }

    public virtual Usuario? SubidaPorNavigation { get; set; }
}
