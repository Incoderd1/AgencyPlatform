using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories;

public class CuponRepository : ICuponRepository
{
    private readonly AgencyPlatformDbContext _context;

    public CuponRepository(AgencyPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cupone>> ObtenerActivosAsync(DateTime? fecha = null)
    {
        var now = fecha ?? DateTime.UtcNow;
        return await _context.Cupones
            .Where(c => c.Estado == "activo" && c.FechaInicio <= now && c.FechaFin >= now)
            .ToListAsync();
    }

    public async Task<Cupone?> ObtenerPorIdAsync(int id)
    {
        return await _context.Cupones.FirstOrDefaultAsync(c => c.IdCupon == id);
    }

    public async Task<Cupone?> ObtenerPorCodigoAsync(string codigo)
    {
        return await _context.Cupones.FirstOrDefaultAsync(c => c.Codigo == codigo);
    }

    public async Task CrearAsync(Cupone cupone)
    {
        _context.Cupones.Add(cupone);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ValidarDisponibilidadAsync(int idCupon, int idCliente)
    {
        var cupon = await ObtenerPorIdAsync(idCupon);
        if (cupon == null) return false;

        // Validaciones básicas
        if (cupon.Estado != "activo") return false;
        if (cupon.FechaInicio > DateTime.UtcNow || cupon.FechaFin < DateTime.UtcNow) return false;
        if (cupon.UsosMaximos.HasValue && cupon.UsosActuales >= cupon.UsosMaximos) return false;

        // Validación por cliente se implementará en cupones_cliente (próximo paso)
        return true;
    }

    public async Task IncrementarUsoAsync(int idCupon)
    {
        var cupon = await _context.Cupones.FirstOrDefaultAsync(c => c.IdCupon == idCupon);
        if (cupon != null)
        {
            cupon.UsosActuales++;
            await _context.SaveChangesAsync();
        }
    }
}
