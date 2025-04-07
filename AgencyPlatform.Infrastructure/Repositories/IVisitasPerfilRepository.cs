using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IVisitasPerfilRepository
    {
        Task<List<VisitasPerfil>> GetAllAsync();
        Task<VisitasPerfil?> GetByIdAsync(int id);
        Task AddAsync(VisitasPerfil entidad);
        void Update(VisitasPerfil entidad);
        void Delete(VisitasPerfil entidad);
        Task SaveChangesAsync();

        Task<int> ContarVisitasPorPerfil(int idPerfil);

    }
}
