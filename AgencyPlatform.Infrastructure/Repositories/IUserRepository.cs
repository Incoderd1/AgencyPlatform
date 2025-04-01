using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<usuario?> GetByEmailAsync(string email);
        Task AddAsync(usuario user);
        Task SaveChangesAsync();
    }
}
