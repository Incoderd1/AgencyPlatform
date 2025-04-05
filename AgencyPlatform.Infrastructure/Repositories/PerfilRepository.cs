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

        public async Task<perfile?> GetByIdAsync(int id)
        {
            return await _context.perfiles.FindAsync(id);
        }

        public async Task AddAsync(perfile perfil)
        {
            await _context.perfiles.AddAsync(perfil);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<perfile> Query()
        {
            return _context.perfiles.AsQueryable();
        }

        public async Task<List<perfile>> GetPaginatedAsync(int page, int pageSize)
        {
            return await _context.perfiles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public void Remove(perfile perfil)
        {
            _context.perfiles.Remove(perfil);  // Llamada al método Remove de DbSet
        }
    }
}
