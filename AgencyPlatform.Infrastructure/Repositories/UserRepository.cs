using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public UserRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public IQueryable<usuario> Query()
        {
            return _context.usuarios.AsQueryable();
        }

        public async Task<usuario> GetByIdAsync(int id)
        {
            return await _context.usuarios.FindAsync(id);
        }

        public async Task<usuario> GetByEmailAsync(string email)
        {
            return await _context.usuarios
                .FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task AddAsync(usuario entity)
        {
            await _context.usuarios.AddAsync(entity);
        }

        public void Update(usuario entity)
        {
            _context.usuarios.Update(entity);
        }

        public void Delete(usuario entity)
        {
            _context.usuarios.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<usuario, bool>> predicate)
        {
            return await _context.usuarios.AnyAsync(predicate);
        }
    }
}