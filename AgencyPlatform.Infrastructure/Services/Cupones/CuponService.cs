using AgencyPlatform.Application.DTOs.Cupones;
using AgencyPlatform.Application.Interfaces.Services.Cupones;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Services.Cupones
{
    public class CuponService : ICuponService
    {
        private readonly AgencyPlatformDbContext _context;

        public CuponService(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<CuponDto>> ObtenerDisponiblesAsync()
        {
            var cupones = await _context.Cupones
                .Where(c => c.Estado == "activo" && c.FechaInicio <= DateTime.UtcNow && c.FechaFin >= DateTime.UtcNow)
                .ToListAsync();

            return cupones.Select(c => new CuponDto
            {
                IdCupon = c.IdCupon,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                TipoDescuento = c.TipoDescuento,
                Valor = c.Valor,
                FechaInicio = c.FechaInicio,
                FechaFin = c.FechaFin,
                MinimoCompra = c.MinimoCompra,
                MaximoDescuento = c.MaximoDescuento,
                UsosMaximos = c.UsosMaximos,
                UsosActuales = c.UsosActuales,
                UsosPorCliente = c.UsosPorCliente,
                AplicaA = c.AplicaA,
                RequierePuntos = c.RequierePuntos,
                Estado = c.Estado,
                FechaCreacion = c.FechaCreacion
            }).ToList();
        }

        public async Task<CuponDto?> ObtenerPorCodigoAsync(string codigo)
        {
            var c = await _context.Cupones.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (c == null) return null;

            return new CuponDto
            {
                IdCupon = c.IdCupon,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                TipoDescuento = c.TipoDescuento,
                Valor = c.Valor,
                FechaInicio = c.FechaInicio,
                FechaFin = c.FechaFin,
                MinimoCompra = c.MinimoCompra,
                MaximoDescuento = c.MaximoDescuento,
                UsosMaximos = c.UsosMaximos,
                UsosActuales = c.UsosActuales,
                UsosPorCliente = c.UsosPorCliente,
                AplicaA = c.AplicaA,
                RequierePuntos = c.RequierePuntos,
                Estado = c.Estado,
                FechaCreacion = c.FechaCreacion,
            };
        }

        public async Task CrearAsync(CrearCuponDto dto)
        {
            var entity = new Cupone
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                TipoDescuento = dto.TipoDescuento,
                Valor = dto.Valor,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                MinimoCompra = dto.MinimoCompra,
                MaximoDescuento = dto.MaximoDescuento,
                UsosMaximos = dto.UsosMaximos,
                UsosActuales = 0,
                UsosPorCliente = dto.UsosPorCliente,
                AplicaA = dto.AplicaA,
                RequierePuntos = dto.RequierePuntos,
                Estado = "activo",
                FechaCreacion = DateTime.UtcNow
            };

            _context.Cupones.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CanjearCuponAsync(CanjearCuponDto dto)
        {
            var cupon = await _context.Cupones.FirstOrDefaultAsync(x => x.Codigo == dto.Codigo);
            if (cupon == null || cupon.Estado != "activo")
                return false;

            // Validaciones adicionales si lo deseas (fechas, usos máximos, etc.)
            cupon.UsosActuales++;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
