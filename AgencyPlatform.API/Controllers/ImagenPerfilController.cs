using AgencyPlatform.Application.DTOs.PerfilImagen;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class ImagenPerfilController : ControllerBase
{
    private readonly IImagenPerfilService _imagenPerfilService;
    private readonly ILogger<ImagenPerfilController> _logger;
    private readonly AgencyPlatformDbContext _context;

    public ImagenPerfilController(IImagenPerfilService imagenPerfilService, ILogger<ImagenPerfilController> logger, AgencyPlatformDbContext context)
    {
        _imagenPerfilService = imagenPerfilService;
        _logger = logger;
        _context = context;
    }

    [HttpGet("perfil/{perfilId}")]
    public async Task<IActionResult> GetByPerfilId(int perfilId)
    {
        var result = await _imagenPerfilService.GetByPerfilAsync(perfilId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _imagenPerfilService.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Imagen no encontrada." });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearImagenPerfilDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest(new { error = true, message = "El DTO no puede ser null" });

            // Llamamos al servicio que ya maneja la asignación del IdPerfil automáticamente
            var result = await _imagenPerfilService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdImagen }, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = true, message = "Error interno", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateImagenPerfilDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest(new { error = true, message = "El DTO no puede ser null" });

            var result = await _imagenPerfilService.UpdateAsync(id, dto);
            if (result == null)
                return NotFound(new { message = "Imagen no encontrada para actualizar." });

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning($"Acceso no autorizado: {ex.Message}");
            return StatusCode(403, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error inesperado al actualizar: {ex.Message}");
            return StatusCode(500, new { error = true, message = "Error interno del servidor", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _imagenPerfilService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = "Imagen no encontrada para eliminar." });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning($"Acceso no autorizado al eliminar: {ex.Message}");
            return StatusCode(403, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar: {ex.Message}");
            return StatusCode(500, new { error = true, message = "Error interno del servidor", details = ex.Message });
        }
    }

    [HttpPost("upload")]
    [AllowAnonymous]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadFileDto dto)
    {
        try
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest(new { message = "Archivo inválido o vacío." });

            var fileName = Path.GetRandomFileName() + Path.GetExtension(dto.File.FileName);
            var today = DateTime.UtcNow;
            var folder = Path.Combine("wwwroot", "uploads", today.Year.ToString(), today.Month.ToString("D2"), today.Day.ToString("D2"));
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), folder);
            Directory.CreateDirectory(absolutePath);

            var filePath = Path.Combine(absolutePath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.File.CopyToAsync(stream);

            var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{today.Year}/{today.Month:D2}/{today.Day:D2}/{fileName}";
            return Ok(new { url = imageUrl });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al subir imagen: {ex.Message}");
            return StatusCode(500, new { error = true, message = "Error al subir archivo", details = ex.Message });
        }
    }

    
}
