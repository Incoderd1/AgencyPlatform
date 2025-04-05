using AgencyPlatform.Application.DTOs.PerfilImagen;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AgencyPlatform.Infrastructure.Services;

public class ImagenPerfilService : IImagenPerfilService
{
    private readonly AgencyPlatformDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImagenPerfilService(AgencyPlatformDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("Usuario no autenticado.");

        return int.Parse(userIdClaim);
    }

    public async Task<ImagenPerfilDto> CreateAsync(CrearImagenPerfilDto dto)
    {
        // Obtener el ID del usuario autenticado desde el token (en lugar de recibirlo en el DTO)
        var userId = GetUserId();

        // Obtener el perfil asociado a ese usuario
        var perfil = await _context.perfiles
            .FirstOrDefaultAsync(p => p.id_usuario == userId);

        if (perfil == null)
            throw new Exception("Perfil no encontrado o no pertenece al usuario actual.");

        // Crear la imagen de perfil con el id del perfil encontrado
        var imagen = new imagenes_perfil
        {
            id_perfil = perfil.id_perfil, // Asignamos automáticamente el id del perfil
            uuid_imagen = Guid.NewGuid(),
            url_imagen = dto.UrlImagen,
            url_thumbnail = dto.UrlThumbnail,
            url_media = dto.UrlMedia,
            url_webp = dto.UrlWebp,
            tipo_mime = dto.TipoMime,
            ancho = dto.Ancho,
            alto = dto.Alto,
            tamaño_bytes = dto.TamañoBytes,
            descripcion = dto.Descripcion,
            tags = dto.Tags != null ? JsonSerializer.Serialize(dto.Tags) : null,
            metadata_exif = dto.MetadataExif != null ? JsonSerializer.Serialize(dto.MetadataExif) : null,
            es_principal = dto.EsPrincipal ?? false,
            contenido_sensible = dto.ContenidoSensible ?? false,
            nivel_blurring = dto.NivelBlurring ?? 0,
            fecha_subida = DateTime.UtcNow,
            estado = "pendiente_revision",
            subida_por = userId,
            orden = (short)(await _context.imagenes_perfils.CountAsync(i => i.id_perfil == perfil.id_perfil) + 1)
        };

        // Guardar la imagen en la base de datos
        _context.imagenes_perfils.Add(imagen);
        await _context.SaveChangesAsync();

        return MapToDto(imagen);
    }


    public async Task<ImagenPerfilDto> UpdateAsync(int idImagen, UpdateImagenPerfilDto dto)
    {
        var userId = GetUserId();

        var imagen = await _context.imagenes_perfils
            .Include(i => i.id_perfilNavigation)
            .FirstOrDefaultAsync(i => i.id_imagen == idImagen && i.id_perfilNavigation.id_usuario == userId);

        if (imagen == null)
            throw new Exception("Imagen no encontrada o no autorizada.");

        if (dto.EsPrincipal == true && !imagen.es_principal)
        {
            var yaPrincipal = await _context.imagenes_perfils
                .AnyAsync(i => i.id_perfil == imagen.id_perfil && i.es_principal && i.id_imagen != idImagen);
            if (yaPrincipal)
                throw new Exception("Ya existe una imagen principal para este perfil.");
        }

        imagen.descripcion = dto.Descripcion ?? imagen.descripcion;
        imagen.tags = dto.Tags != null ? JsonSerializer.Serialize(dto.Tags) : imagen.tags;
        imagen.metadata_exif = dto.MetadataExif != null ? JsonSerializer.Serialize(dto.MetadataExif) : imagen.metadata_exif;
        imagen.es_principal = dto.EsPrincipal ?? imagen.es_principal;
        imagen.contenido_sensible = dto.ContenidoSensible ?? imagen.contenido_sensible;
        imagen.nivel_blurring = dto.NivelBlurring ?? imagen.nivel_blurring;

        await _context.SaveChangesAsync();
        return MapToDto(imagen);
    }

    public async Task<bool> DeleteAsync(int idImagen)
    {
        var userId = GetUserId();

        var imagen = await _context.imagenes_perfils
            .Include(i => i.id_perfilNavigation)
            .FirstOrDefaultAsync(i => i.id_imagen == idImagen && i.id_perfilNavigation.id_usuario == userId);

        if (imagen == null) return false;

        int idPerfil = imagen.id_perfil;

        _context.imagenes_perfils.Remove(imagen);
        await _context.SaveChangesAsync();

        var imagenesRestantes = await _context.imagenes_perfils
            .Where(i => i.id_perfil == idPerfil)
            .OrderBy(i => i.orden)
            .ToListAsync();

        short nuevoOrden = 1;
        foreach (var img in imagenesRestantes)
        {
            img.orden = nuevoOrden++;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ImagenPerfilDto?> GetByIdAsync(int idImagen)
    {
        var userId = GetUserId();

        var imagen = await _context.imagenes_perfils
            .Include(i => i.id_perfilNavigation)
            .FirstOrDefaultAsync(i => i.id_imagen == idImagen && i.id_perfilNavigation.id_usuario == userId);

        return imagen != null ? MapToDto(imagen) : null;
    }

    public async Task<List<ImagenPerfilDto>> GetByPerfilAsync(int idPerfil)
    {
        var userId = GetUserId();

        var perfil = await _context.perfiles.FirstOrDefaultAsync(p => p.id_perfil == idPerfil && p.id_usuario == userId);
        if (perfil == null)
            throw new Exception("Perfil no encontrado o no autorizado.");

        var imagenes = await _context.imagenes_perfils
            .Where(i => i.id_perfil == idPerfil)
            .OrderBy(i => i.orden)
            .ToListAsync();

        return imagenes.Select(MapToDto).ToList();
    }

    private static ImagenPerfilDto MapToDto(imagenes_perfil imagen) => new()
    {
        IdImagen = imagen.id_imagen,
        UuidImagen = imagen.uuid_imagen,
        UrlImagen = imagen.url_imagen,
        UrlThumbnail = imagen.url_thumbnail,
        UrlMedia = imagen.url_media,
        UrlWebp = imagen.url_webp,
        TipoMime = imagen.tipo_mime,
        Ancho = imagen.ancho,
        Alto = imagen.alto,
        TamañoBytes = imagen.tamaño_bytes,
        Descripcion = imagen.descripcion,
        Tags = imagen.tags != null ? JsonSerializer.Deserialize<object>(imagen.tags) : null,
        MetadataExif = imagen.metadata_exif != null ? JsonSerializer.Deserialize<object>(imagen.metadata_exif) : null,
        EsPrincipal = imagen.es_principal,
        ContenidoSensible = imagen.contenido_sensible,
        NivelBlurring = imagen.nivel_blurring,
        FechaSubida = imagen.fecha_subida,
        Estado = imagen.estado
    };
}
