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

        public IQueryable<Usuario> Query()
        {
            return _context.Usuarios.AsQueryable();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(Usuario entity)
        {
            await _context.Usuarios.AddAsync(entity);
        }

        public void Update(Usuario entity)
        {
            _context.Usuarios.Update(entity);
        }

        public void Delete(Usuario entity)
        {
            _context.Usuarios.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Usuario, bool>> predicate)
        {
            return await _context.Usuarios.AnyAsync(predicate);
        }
    }
}