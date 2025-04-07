using AgencyPlatform.Application.DTOs.Agencia;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace AgencyPlatform.Infrastructure.Services;

public class AgenciaService : IAgenciaService
{
    private readonly AgencyPlatformDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AgenciaService(AgencyPlatformDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    }

    public async Task<IEnumerable<AgenciaDto>> GetAllAsync()
    {
        var agencias = await _context.Agencias.ToListAsync();

        return agencias.Select(a => new AgenciaDto
        {
            Id = a.IdAgencia,
            IdUsuario = a.IdUsuario,
            NombreComercial = a.NombreComercial,
            RazonSocial = a.RazonSocial,
            NifCif = a.NifCif,
            Direccion = a.Direccion,
            Ciudad = a.Ciudad,
            Region = a.Region,
            Pais = a.Pais,
            CodigoPostal = a.CodigoPostal,
            Telefono = a.Telefono,
            EmailContacto = a.EmailContacto,
            SitioWeb = a.SitioWeb,
            Descripcion = a.Descripcion,
            Horario = string.IsNullOrWhiteSpace(a.Horario) ? null : JsonSerializer.Deserialize<HorarioDto>(a.Horario),
            LogoUrl = a.LogoUrl,
            Verificada = a.Verificada,
            FechaVerificacion = a.FechaVerificacion,
            NumPerfilesActivos = a.NumPerfilesActivos,
            FechaRegistro = a.FechaRegistro,
            FechaActualizacion = a.FechaActualizacion,
            Estado = a.Estado
        }).ToList();
    }

    public async Task<AgenciaDto?> GetByIdAsync(int id)
    {
        var agencia = await _context.Agencias.FindAsync(id);
        if (agencia == null) return null;

        return new AgenciaDto
        {
            Id = agencia.IdAgencia,
            IdUsuario = agencia.IdUsuario,
            NombreComercial = agencia.NombreComercial,
            RazonSocial = agencia.RazonSocial,
            NifCif = agencia.NifCif,
            Direccion = agencia.Direccion,
            Ciudad = agencia.Ciudad,
            Region = agencia.Region,
            Pais = agencia.Pais,
            CodigoPostal = agencia.CodigoPostal,
            Telefono = agencia.Telefono,
            EmailContacto = agencia.EmailContacto,
            SitioWeb = agencia.SitioWeb,
            Descripcion = agencia.Descripcion,
            Horario = string.IsNullOrWhiteSpace(agencia.Horario) ? null : JsonSerializer.Deserialize<HorarioDto>(agencia.Horario),
            LogoUrl = agencia.LogoUrl,
            Verificada = agencia.Verificada,
            FechaVerificacion = agencia.FechaVerificacion,
            NumPerfilesActivos = agencia.NumPerfilesActivos,
            FechaRegistro = agencia.FechaRegistro,
            FechaActualizacion = agencia.FechaActualizacion,
            Estado = agencia.Estado
        };
    }

    public async Task<AgenciaDto?> GetByUserIdAsync(int userId)
    {
        var agencia = await _context.Agencias.FirstOrDefaultAsync(a => a.IdUsuario == userId);
        if (agencia == null) return null;

        return new AgenciaDto
        {
            Id = agencia.IdAgencia,
            IdUsuario = agencia.IdUsuario,
            NombreComercial = agencia.NombreComercial,
            RazonSocial = agencia.RazonSocial,
            NifCif = agencia.NifCif,
            Direccion = agencia.Direccion,
            Ciudad = agencia.Ciudad,
            Region = agencia.Region,
            Pais = agencia.Pais,
            CodigoPostal = agencia.CodigoPostal,
            Telefono = agencia.Telefono,
            EmailContacto = agencia.EmailContacto,
            SitioWeb = agencia.SitioWeb,
            Descripcion = agencia.Descripcion,
            Horario = string.IsNullOrWhiteSpace(agencia.Horario) ? null : JsonSerializer.Deserialize<HorarioDto>(agencia.Horario),
            LogoUrl = agencia.LogoUrl,
            Verificada = agencia.Verificada,
            FechaVerificacion = agencia.FechaVerificacion,
            NumPerfilesActivos = agencia.NumPerfilesActivos,
            FechaRegistro = agencia.FechaRegistro,
            FechaActualizacion = agencia.FechaActualizacion,
            Estado = agencia.Estado
        };
    }

    public async Task<AgenciaDto> CreateAsync(CrearAgenciaDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();

            var agencia = new Agencia
            {
                IdUsuario = userId,
                NombreComercial = dto.NombreComercial,
                RazonSocial = dto.RazonSocial,
                NifCif = dto.NifCif,
                Direccion = dto.Direccion,
                Ciudad = dto.Ciudad,
                Region = dto.Region,
                Pais = dto.Pais,
                CodigoPostal = dto.CodigoPostal,
                Telefono = dto.Telefono,
                EmailContacto = dto.EmailContacto,
                SitioWeb = dto.SitioWeb,
                Descripcion = dto.Descripcion,
                Horario = dto.Horario is not null ? JsonSerializer.Serialize(dto.Horario) : null,
                LogoUrl = dto.LogoUrl,
                FechaRegistro = DateTime.UtcNow.ToLocalTime(),
                FechaActualizacion = DateTime.UtcNow.ToLocalTime(),
                Estado = "pendiente_verificacion"
            };

            _context.Agencias.Add(agencia);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(agencia.IdAgencia);
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException?.Message ?? "No inner exception";
            throw new Exception($"❌ Error al guardar agencia: {ex.Message} | Inner: {inner}", ex);
        }
    }

    public async Task<AgenciaDto?> UpdateAsync(int id, UpdateAgenciaDto dto)
    {
        var agencia = await _context.Agencias.FindAsync(id);
        if (agencia == null) return null;

        agencia.NombreComercial = dto.NombreComercial ?? agencia.NombreComercial;
        agencia.RazonSocial = dto.RazonSocial ?? agencia.RazonSocial;
        agencia.NifCif = dto.NifCif ?? agencia.NifCif;
        agencia.Direccion = dto.Direccion ?? agencia.Direccion;
        agencia.Ciudad = dto.Ciudad ?? agencia.Ciudad;
        agencia.Region = dto.Region ?? agencia.Region;
        agencia.Pais = dto.Pais ?? agencia.Pais;
        agencia.CodigoPostal = dto.CodigoPostal ?? agencia.CodigoPostal;
        agencia.Telefono = dto.Telefono ?? agencia.Telefono;
        agencia.EmailContacto = dto.EmailContacto ?? agencia.EmailContacto;
        agencia.SitioWeb = dto.SitioWeb ?? agencia.SitioWeb;
        agencia.Descripcion = dto.Descripcion ?? agencia.Descripcion;
        agencia.Horario = dto.Horario is not null ? JsonSerializer.Serialize(dto.Horario) : agencia.Horario;
        agencia.LogoUrl = dto.LogoUrl ?? agencia.LogoUrl;
        agencia.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(agencia.IdAgencia);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var agencia = await _context.Agencias.FindAsync(id);
        if (agencia == null) return false;

        _context.Agencias.Remove(agencia);
        await _context.SaveChangesAsync();

        return true;
    }
}
