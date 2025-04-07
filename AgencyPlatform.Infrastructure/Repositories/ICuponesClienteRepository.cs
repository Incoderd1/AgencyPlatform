using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface ICuponesClienteRepository
    {
        Task<List<CuponesCliente>> GetAllAsync();
        Task<CuponesCliente?> GetByIdAsync(int id);
        Task AddAsync(CuponesCliente entity);
        Task UpdateAsync(CuponesCliente entity);
        Task DeleteAsync(CuponesCliente entity);
        Task SaveAsync();
    }
}
