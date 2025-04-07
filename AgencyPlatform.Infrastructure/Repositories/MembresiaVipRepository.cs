using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class MembresiaVipRepository : IMembresiaVipRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public MembresiaVipRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<MembresiasVip>> GetAllAsync()
        {
            return await _context.MembresiasVips
                .OrderByDescending(m => m.FechaCreacion)
                .ToListAsync();
        }

        public async Task<MembresiasVip?> GetByIdAsync(int id)
        {
            return await _context.MembresiasVips.FindAsync(id);
        }

        public async Task AddAsync(MembresiasVip entidad)
        {
            await _context.MembresiasVips.AddAsync(entidad);
        }

        public void Update(MembresiasVip entidad)
        {
            _context.MembresiasVips.Update(entidad);
        }

        public void Delete(MembresiasVip entidad)
        {
            _context.MembresiasVips.Remove(entidad);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
