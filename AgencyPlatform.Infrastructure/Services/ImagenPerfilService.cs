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
        var perfil = await _context.Perfiles
            .FirstOrDefaultAsync(p => p.IdUsuario == userId);

        if (perfil == null)
            throw new Exception("Perfil no encontrado o no pertenece al usuario actual.");

        // Crear la imagen de perfil con el id del perfil encontrado
        var imagen = new ImagenesPerfil
        {
            IdPerfil = perfil.IdPerfil, // Asignamos automáticamente el id del perfil
            UuidImagen = Guid.NewGuid(),
            UrlImagen = dto.UrlImagen,
            UrlThumbnail = dto.UrlThumbnail,
            UrlMedia = dto.UrlMedia,
            UrlWebp = dto.UrlWebp,
            TipoMime = dto.TipoMime,
            Ancho = dto.Ancho,
            Alto = dto.Alto,
            TamañoBytes = dto.TamañoBytes,
            Descripcion = dto.Descripcion,
            Tags = dto.Tags != null ? JsonSerializer.Serialize(dto.Tags) : null,
            MetadataExif = dto.MetadataExif != null ? JsonSerializer.Serialize(dto.MetadataExif) : null,
            EsPrincipal = dto.EsPrincipal ?? false,
            ContenidoSensible = dto.ContenidoSensible ?? false,
            NivelBlurring = dto.NivelBlurring ?? 0,
            FechaSubida = DateTime.UtcNow,
            Estado = "pendiente_revision",
            SubidaPor = userId,
            Orden = (short)(await _context.ImagenesPerfils.CountAsync(i => i.IdPerfil == perfil.IdPerfil) + 1)
        };

        // Guardar la imagen en la base de datos
        _context.ImagenesPerfils.Add(imagen);
        await _context.SaveChangesAsync();

        return MapToDto(imagen);
    }


    public async Task<ImagenPerfilDto> UpdateAsync(int idImagen, UpdateImagenPerfilDto dto)
    {
        var userId = GetUserId();

        var imagen = await _context.ImagenesPerfils
            .Include(i => i.IdPerfilNavigation)
            .FirstOrDefaultAsync(i => i.IdImagen == idImagen && i.IdPerfilNavigation.IdUsuario == userId);

        if (imagen == null)
            throw new Exception("Imagen no encontrada o no autorizada.");

        if (dto.EsPrincipal == true && !imagen.EsPrincipal)
        {
            var yaPrincipal = await _context.ImagenesPerfils
                .AnyAsync(i => i.IdPerfil == imagen.IdPerfil && i.EsPrincipal && i.IdImagen != idImagen);
            if (yaPrincipal)
                throw new Exception("Ya existe una imagen principal para este perfil.");
        }

        imagen.Descripcion = dto.Descripcion ?? imagen.Descripcion;
        imagen.Tags = dto.Tags != null ? JsonSerializer.Serialize(dto.Tags) : imagen.Tags;
        imagen.MetadataExif = dto.MetadataExif != null ? JsonSerializer.Serialize(dto.MetadataExif) : imagen.MetadataExif;
        imagen.EsPrincipal = dto.EsPrincipal ?? imagen.EsPrincipal;
        imagen.ContenidoSensible = dto.ContenidoSensible ?? imagen.ContenidoSensible;
        imagen.NivelBlurring = dto.NivelBlurring ?? imagen.NivelBlurring;

        await _context.SaveChangesAsync();
        return MapToDto(imagen);
    }

    public async Task<bool> DeleteAsync(int idImagen)
    {
        var userId = GetUserId();

        var imagen = await _context.ImagenesPerfils
            .Include(i => i.IdPerfilNavigation)
            .FirstOrDefaultAsync(i => i.IdImagen == idImagen && i.IdPerfilNavigation.IdUsuario == userId);

        if (imagen == null) return false;

        int idPerfil = imagen.IdPerfil;

        _context.ImagenesPerfils.Remove(imagen);
        await _context.SaveChangesAsync();

        var imagenesRestantes = await _context.ImagenesPerfils
            .Where(i => i.IdPerfil == idPerfil)
            .OrderBy(i => i.Orden)
            .ToListAsync();

        short nuevoOrden = 1;
        foreach (var img in imagenesRestantes)
        {
            img.Orden = nuevoOrden++;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ImagenPerfilDto?> GetByIdAsync(int idImagen)
    {
        var userId = GetUserId();

        var imagen = await _context.ImagenesPerfils
            .Include(i => i.IdPerfilNavigation)
            .FirstOrDefaultAsync(i => i.IdImagen == idImagen && i.IdPerfilNavigation.IdUsuario == userId);

        return imagen != null ? MapToDto(imagen) : null;
    }

    public async Task<List<ImagenPerfilDto>> GetByPerfilAsync(int idPerfil)
    {
        var userId = GetUserId();

        var perfil = await _context.Perfiles.FirstOrDefaultAsync(p => p.IdPerfil == idPerfil && p.IdUsuario == userId);
        if (perfil == null)
            throw new Exception("Perfil no encontrado o no autorizado.");

        var imagenes = await _context.ImagenesPerfils
            .Where(i => i.IdPerfil == idPerfil)
            .OrderBy(i => i.Orden)
            .ToListAsync();

        return imagenes.Select(MapToDto).ToList();
    }

    private static ImagenPerfilDto MapToDto(ImagenesPerfil imagen) => new()
    {
        IdImagen = imagen.IdImagen,
        UuidImagen = imagen.UuidImagen,
        UrlImagen = imagen.UrlImagen,
        UrlThumbnail = imagen.UrlThumbnail,
        UrlMedia = imagen.UrlMedia,
        UrlWebp = imagen.UrlWebp,
        TipoMime = imagen.TipoMime,
        Ancho = imagen.Ancho,
        Alto = imagen.Alto,
        TamañoBytes = imagen.TamañoBytes,
        Descripcion = imagen.Descripcion,
        Tags = imagen.Tags != null ? JsonSerializer.Deserialize<object>(imagen.Tags) : null,
        MetadataExif = imagen.MetadataExif != null ? JsonSerializer.Deserialize<object>(imagen.MetadataExif) : null,
        EsPrincipal = imagen.EsPrincipal,
        ContenidoSensible = imagen.ContenidoSensible,
        NivelBlurring = imagen.NivelBlurring,
        FechaSubida = imagen.FechaSubida,
        Estado = imagen.Estado
    };
}
