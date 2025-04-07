// SuscripcionVipRepository.cs
using AgencyPlatform.Application.Interfaces.Repositories;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class SuscripcionVipRepository : ISuscripcionVipRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public SuscripcionVipRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<SuscripcionesVip>> GetAllAsync()
        {
            return await _context.SuscripcionesVips
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdPlanNavigation)
                .ToListAsync();
        }

        public async Task<SuscripcionesVip?> GetByIdAsync(int id)
        {
            return await _context.SuscripcionesVips
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdPlanNavigation)
                .FirstOrDefaultAsync(x => x.IdSuscripcion == id);
        }

        public async Task AddAsync(SuscripcionesVip entidad)
        {
            await _context.SuscripcionesVips.AddAsync(entidad);
        }

        public void Update(SuscripcionesVip entidad)
        {
            _context.SuscripcionesVips.Update(entidad);
        }

        public void Delete(SuscripcionesVip entidad)
        {
            _context.SuscripcionesVips.Remove(entidad);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
