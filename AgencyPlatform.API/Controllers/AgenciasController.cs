using AgencyPlatform.Application.DTOs.Agencia;
using AgencyPlatform.Application.Interfaces.Services;
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
        var agencias = await _context.agencias.ToListAsync();

        return agencias.Select(a => new AgenciaDto
        {
            Id = a.id_agencia,
            IdUsuario = a.id_usuario,
            NombreComercial = a.nombre_comercial,
            RazonSocial = a.razon_social,
            NifCif = a.nif_cif,
            Direccion = a.direccion,
            Ciudad = a.ciudad,
            Region = a.region,
            Pais = a.pais,
            CodigoPostal = a.codigo_postal,
            Telefono = a.telefono,
            EmailContacto = a.email_contacto,
            SitioWeb = a.sitio_web,
            Descripcion = a.descripcion,
            Horario = string.IsNullOrWhiteSpace(a.horario) ? null : JsonSerializer.Deserialize<HorarioDto>(a.horario),
            LogoUrl = a.logo_url,
            Verificada = a.verificada,
            FechaVerificacion = a.fecha_verificacion,
            NumPerfilesActivos = a.num_perfiles_activos,
            FechaRegistro = a.fecha_registro,
            FechaActualizacion = a.fecha_actualizacion,
            Estado = a.estado
        }).ToList();
    }

    public async Task<AgenciaDto?> GetByIdAsync(int id)
    {
        var agencia = await _context.agencias.FindAsync(id);
        if (agencia == null) return null;

        return new AgenciaDto
        {
            Id = agencia.id_agencia,
            IdUsuario = agencia.id_usuario,
            NombreComercial = agencia.nombre_comercial,
            RazonSocial = agencia.razon_social,
            NifCif = agencia.nif_cif,
            Direccion = agencia.direccion,
            Ciudad = agencia.ciudad,
            Region = agencia.region,
            Pais = agencia.pais,
            CodigoPostal = agencia.codigo_postal,
            Telefono = agencia.telefono,
            EmailContacto = agencia.email_contacto,
            SitioWeb = agencia.sitio_web,
            Descripcion = agencia.descripcion,
            Horario = string.IsNullOrWhiteSpace(agencia.horario) ? null : JsonSerializer.Deserialize<HorarioDto>(agencia.horario),
            LogoUrl = agencia.logo_url,
            Verificada = agencia.verificada,
            FechaVerificacion = agencia.fecha_verificacion,
            NumPerfilesActivos = agencia.num_perfiles_activos,
            FechaRegistro = agencia.fecha_registro,
            FechaActualizacion = agencia.fecha_actualizacion,
            Estado = agencia.estado
        };
    }

    public async Task<AgenciaDto?> GetByUserIdAsync(int userId)
    {
        var agencia = await _context.agencias.FirstOrDefaultAsync(a => a.id_usuario == userId);
        if (agencia == null) return null;

        return new AgenciaDto
        {
            Id = agencia.id_agencia,
            IdUsuario = agencia.id_usuario,
            NombreComercial = agencia.nombre_comercial,
            RazonSocial = agencia.razon_social,
            NifCif = agencia.nif_cif,
            Direccion = agencia.direccion,
            Ciudad = agencia.ciudad,
            Region = agencia.region,
            Pais = agencia.pais,
            CodigoPostal = agencia.codigo_postal,
            Telefono = agencia.telefono,
            EmailContacto = agencia.email_contacto,
            SitioWeb = agencia.sitio_web,
            Descripcion = agencia.descripcion,
            Horario = string.IsNullOrWhiteSpace(agencia.horario) ? null : JsonSerializer.Deserialize<HorarioDto>(agencia.horario),
            LogoUrl = agencia.logo_url,
            Verificada = agencia.verificada,
            FechaVerificacion = agencia.fecha_verificacion,
            NumPerfilesActivos = agencia.num_perfiles_activos,
            FechaRegistro = agencia.fecha_registro,
            FechaActualizacion = agencia.fecha_actualizacion,
            Estado = agencia.estado
        };
    }

    public async Task<AgenciaDto> CreateAsync(CrearAgenciaDto dto)
    {
        try
        {
            var userId = GetCurrentUserId();

            var agencia = new agencia
            {
                id_usuario = userId,
                nombre_comercial = dto.NombreComercial,
                razon_social = dto.RazonSocial,
                nif_cif = dto.NifCif,
                direccion = dto.Direccion,
                ciudad = dto.Ciudad,
                region = dto.Region,
                pais = dto.Pais,
                codigo_postal = dto.CodigoPostal,
                telefono = dto.Telefono,
                email_contacto = dto.EmailContacto,
                sitio_web = dto.SitioWeb,
                descripcion = dto.Descripcion,
                horario = dto.Horario is not null ? JsonSerializer.Serialize(dto.Horario) : null,
                logo_url = dto.LogoUrl,
                fecha_registro = DateTime.UtcNow.ToLocalTime(),
                fecha_actualizacion = DateTime.UtcNow.ToLocalTime(),
                estado = "pendiente_verificacion"
            };

            _context.agencias.Add(agencia);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(agencia.id_agencia);
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException?.Message ?? "No inner exception";
            throw new Exception($"❌ Error al guardar agencia: {ex.Message} | Inner: {inner}", ex);
        }
    }

    public async Task<AgenciaDto?> UpdateAsync(int id, UpdateAgenciaDto dto)
    {
        var agencia = await _context.agencias.FindAsync(id);
        if (agencia == null) return null;

        agencia.nombre_comercial = dto.NombreComercial ?? agencia.nombre_comercial;
        agencia.razon_social = dto.RazonSocial ?? agencia.razon_social;
        agencia.nif_cif = dto.NifCif ?? agencia.nif_cif;
        agencia.direccion = dto.Direccion ?? agencia.direccion;
        agencia.ciudad = dto.Ciudad ?? agencia.ciudad;
        agencia.region = dto.Region ?? agencia.region;
        agencia.pais = dto.Pais ?? agencia.pais;
        agencia.codigo_postal = dto.CodigoPostal ?? agencia.codigo_postal;
        agencia.telefono = dto.Telefono ?? agencia.telefono;
        agencia.email_contacto = dto.EmailContacto ?? agencia.email_contacto;
        agencia.sitio_web = dto.SitioWeb ?? agencia.sitio_web;
        agencia.descripcion = dto.Descripcion ?? agencia.descripcion;
        agencia.horario = dto.Horario is not null ? JsonSerializer.Serialize(dto.Horario) : agencia.horario;
        agencia.logo_url = dto.LogoUrl ?? agencia.logo_url;
        agencia.fecha_actualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(agencia.id_agencia);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var agencia = await _context.agencias.FindAsync(id);
        if (agencia == null) return false;

        _context.agencias.Remove(agencia);
        await _context.SaveChangesAsync();

        return true;
    }
}
