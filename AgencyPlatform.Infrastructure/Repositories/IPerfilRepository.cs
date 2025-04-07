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
        Task<Perfile?> GetByIdAsync(int id);
        Task AddAsync(Perfile perfil);
        Task SaveChangesAsync();
        IQueryable<Perfile> Query();
        Task<List<Perfile>> GetPaginatedAsync(int page, int pageSize);
        void Remove(Perfile perfil);  // Agrega este método

    }
}
