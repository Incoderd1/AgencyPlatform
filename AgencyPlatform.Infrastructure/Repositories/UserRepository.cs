using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public UserRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<usuario?> GetByEmailAsync(string email)
        {
            return await _context.usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task AddAsync(usuario user)
        {
            await _context.usuarios.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
