using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class VisitasPerfilRepository : IVisitasPerfilRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public VisitasPerfilRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<VisitasPerfil>> GetAllAsync()
        {
            return await _context.VisitasPerfils
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdPerfilNavigation)
                .ToListAsync();
        }

        public async Task<VisitasPerfil?> GetByIdAsync(int id)
        {
            return await _context.VisitasPerfils
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdPerfilNavigation)
                .FirstOrDefaultAsync(v => v.IdVisita == id);
        }

        public async Task AddAsync(VisitasPerfil entidad)
        {
            await _context.VisitasPerfils.AddAsync(entidad);
        }

        public void Update(VisitasPerfil entidad)
        {
            _context.VisitasPerfils.Update(entidad);
        }

        public void Delete(VisitasPerfil entidad)
        {
            _context.VisitasPerfils.Remove(entidad);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Implementación del método para contar visitas por perfil
        public async Task<int> ContarVisitasPorPerfil(int idPerfil)
        {
            return await _context.VisitasPerfils
                .Where(v => v.IdPerfil == idPerfil)
                .CountAsync();
        }
    }
}
