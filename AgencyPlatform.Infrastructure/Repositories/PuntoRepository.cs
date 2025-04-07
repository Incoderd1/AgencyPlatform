using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories;

public class PuntoRepository : IPuntoRepository
{
    private readonly AgencyPlatformDbContext _context;

    public PuntoRepository(AgencyPlatformDbContext context)
    {
        _context = context;
    }

    public async Task CrearAsync(Punto punto)
    {
        _context.Puntos.Add(punto);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Punto>> ObtenerPorClienteAsync(int idCliente)
    {
        return await _context.Puntos
            .Where(p => p.IdCliente == idCliente)
            .OrderByDescending(p => p.FechaObtencion)
            .ToListAsync();
    }

    public async Task<(int total, int usados, int expirados)> ObtenerResumenAsync(int idCliente)
    {
        var puntos = await _context.Puntos
            .Where(p => p.IdCliente == idCliente)
            .ToListAsync();

        int total = puntos.Sum(p => p.Cantidad);
        int usados = puntos.Where(p => p.Estado == "usado").Sum(p => p.Cantidad);
        int expirados = puntos.Where(p => p.Estado == "expirado").Sum(p => p.Cantidad);

        return (total, usados, expirados);
    }
}
