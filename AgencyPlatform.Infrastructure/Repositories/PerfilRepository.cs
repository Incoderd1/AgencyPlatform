using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public PerfilRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<Perfile?> GetByIdAsync(int id)
        {
            return await _context.Perfiles.FindAsync(id);
        }

        public async Task AddAsync(Perfile perfil)
        {
            await _context.Perfiles.AddAsync(perfil);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<Perfile> Query()
        {
            return _context.Perfiles.AsQueryable();
        }

        public async Task<List<Perfile>> GetPaginatedAsync(int page, int pageSize)
        {
            return await _context.Perfiles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public void Remove(Perfile perfil)
        {
            _context.Perfiles.Remove(perfil);  // Llamada al método Remove de DbSet
        }
    }
}
