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
        IQueryable<Usuario> Query();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByEmailAsync(string email);
        Task AddAsync(Usuario entity);
        void Update(Usuario entity);
        void Delete(Usuario entity);
        Task SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<Usuario, bool>> predicate);


    }
}
