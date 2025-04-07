using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface ISuscripcionVipRepository
    {
        Task<List<SuscripcionesVip>> GetAllAsync();
        Task<SuscripcionesVip?> GetByIdAsync(int id);
        Task AddAsync(SuscripcionesVip entidad);
        void Update(SuscripcionesVip entidad);
        void Delete(SuscripcionesVip entidad);
        Task SaveChangesAsync();
    }
}
