using AgencyPlatform.Application.Interfaces.Repositories;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace AgencyPlatform.Infrastructure.Repositories
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public AgenciaRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<agencia>> GetAllAsync()
        {
            return await _context.agencias.AsNoTracking().ToListAsync();
        }

        public async Task<agencia?> GetByIdAsync(int id)
        {
            return await _context.agencias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.id_agencia == id);
        }

        public async Task<agencia?> GetByUserIdAsync(int userId)
        {
            return await _context.agencias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.id_usuario == userId);
        }

        public async Task<agencia> AddAsync(agencia agencia)
        {
            await _context.agencias.AddAsync(agencia);
            await _context.SaveChangesAsync();
            return agencia;
        }

        public async Task<agencia?> UpdateAsync(agencia agencia)
        {
            _context.agencias.Update(agencia);
            await _context.SaveChangesAsync();
            return agencia;
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
}
