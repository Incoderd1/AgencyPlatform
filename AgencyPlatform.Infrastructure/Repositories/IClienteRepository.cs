using AgencyPlatform.Application.DTOs.Clientes;
using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IClienteRepository
    {
        Task AddAsync(Cliente entity);
        void Delete(Cliente entity);
        Task<Cliente?> GetByIdAsync(int id);
        Task<ClienteResumenDto?> GetResumenByIdAsync(int id);
        Task<List<ClienteResumenDto>> GetAllResumenAsync();
        IQueryable<ClienteResumenDto> QueryResumen();

        Task SaveChangesAsync();
    }
}
