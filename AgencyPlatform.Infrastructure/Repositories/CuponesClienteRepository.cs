using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class CuponesClienteRepository : ICuponesClienteRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public CuponesClienteRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<CuponesCliente>> GetAllAsync()
        {
            return await _context.CuponesClientes.ToListAsync();
        }

        public async Task<CuponesCliente?> GetByIdAsync(int id)
        {
            return await _context.CuponesClientes.FindAsync(id);
        }

        public async Task AddAsync(CuponesCliente entity)
        {
            await _context.CuponesClientes.AddAsync(entity);
        }

        public async Task UpdateAsync(CuponesCliente entity)
        {
            _context.CuponesClientes.Update(entity);
        }

        public async Task DeleteAsync(CuponesCliente entity)
        {
            _context.CuponesClientes.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
