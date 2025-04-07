using AgencyPlatform.Application.DTOs.Puntos;
using AgencyPlatform.Application.Interfaces.Services.Puntos;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;

namespace AgencyPlatform.Infrastructure.Services;

public class PuntoService : IPuntoService
{
    private readonly IPuntoRepository _puntoRepository;

    public PuntoService(IPuntoRepository puntoRepository)
    {
        _puntoRepository = puntoRepository;
    }

    public async Task CrearAsync(CrearPuntoDto dto)
    {
        var punto = new Punto
        {
            IdCliente = dto.IdCliente,
            Cantidad = dto.Cantidad,
            TipoAccion = dto.TipoAccion,
            Descripcion = dto.Descripcion,
            FechaObtencion = DateTime.UtcNow,
            FechaExpiracion = dto.FechaExpiracion,
            Estado = "activo",
            IdReferencia = dto.IdReferencia,
            TipoReferencia = dto.TipoReferencia,
            Multiplicador = dto.Multiplicador > 0 ? dto.Multiplicador : 1.0m
        };

        await _puntoRepository.CrearAsync(punto);
    }

    public async Task<List<PuntoDto>> ObtenerPorClienteAsync(int idCliente)
    {
        var puntos = await _puntoRepository.ObtenerPorClienteAsync(idCliente);

        return puntos.Select(p => new PuntoDto
        {
            IdPunto = p.IdPunto,
            IdCliente = p.IdCliente,
            Cantidad = p.Cantidad,
            TipoAccion = p.TipoAccion,
            Descripcion = p.Descripcion,
            FechaObtencion = p.FechaObtencion,
            FechaExpiracion = p.FechaExpiracion,
            Estado = p.Estado,
            IdReferencia = p.IdReferencia,
            TipoReferencia = p.TipoReferencia,
            Multiplicador = p.Multiplicador
        }).ToList();
    }

    public async Task<ResumenPuntosDto> ObtenerResumenAsync(int idCliente)
    {
        var (total, usados, expirados) = await _puntoRepository.ObtenerResumenAsync(idCliente);

        return new ResumenPuntosDto
        {
            TotalAcumulado = total,
            TotalUsado = usados,
            TotalExpirado = expirados
        };
    }
}
