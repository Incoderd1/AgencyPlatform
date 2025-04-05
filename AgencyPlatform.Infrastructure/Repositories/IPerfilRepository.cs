using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IPerfilRepository
    {
        Task<perfile?> GetByIdAsync(int id);
        Task AddAsync(perfile perfil);
        Task SaveChangesAsync();
        IQueryable<perfile> Query();
        Task<List<perfile>> GetPaginatedAsync(int page, int pageSize);
        void Remove(perfile perfil);  // Agrega este método

    }
}
