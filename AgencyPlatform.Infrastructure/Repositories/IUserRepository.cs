using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        IQueryable<usuario> Query();
        Task<usuario> GetByIdAsync(int id);
        Task<usuario> GetByEmailAsync(string email);
        Task AddAsync(usuario entity);
        void Update(usuario entity);
        void Delete(usuario entity);
        Task SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<usuario, bool>> predicate);


    }
}
