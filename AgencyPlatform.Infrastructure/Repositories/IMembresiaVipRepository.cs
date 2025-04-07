using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IMembresiaVipRepository
    {
        Task<List<MembresiasVip>> GetAllAsync();
        Task<MembresiasVip?> GetByIdAsync(int id);
        Task AddAsync(MembresiasVip entidad);
        void Update(MembresiasVip entidad);
        void Delete(MembresiasVip entidad);
        Task SaveChangesAsync();
    }
}
