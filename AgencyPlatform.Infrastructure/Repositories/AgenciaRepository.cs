using AgencyPlatform.Application.Interfaces.Repositories;
using AgencyPlatform.Infrastructure.Data;
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

        public async Task<IEnumerable<Agencia>> GetAllAsync()
        {
            return await _context.Agencias.AsNoTracking().ToListAsync();
        }

        public async Task<Agencia?> GetByIdAsync(int id)
        {
            return await _context.Agencias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdUsuario == id);
        }

        public async Task<Agencia?> GetByUserIdAsync(int userId)
        {
            return await _context.Agencias
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdUsuario == userId);
        }

        public async Task<Agencia> AddAsync(Agencia agencia)
        {
            await _context.Agencias.AddAsync(agencia);
            await _context.SaveChangesAsync();
            return agencia;
        }

        public async Task<Agencia?> UpdateAsync(Agencia agencia)
        {
            _context.Agencias.Update(agencia);
            await _context.SaveChangesAsync();
            return agencia;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null) return false;

            _context.Agencias.Remove(agencia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
