using AgencyPlatform.Infrastructure.Data.Entities;


namespace AgencyPlatform.Application.Interfaces.Repositories
{
    public interface IAgenciaRepository
    {
        Task<IEnumerable<agencia>> GetAllAsync();
        Task<agencia?> GetByIdAsync(int id);
        Task<agencia?> GetByUserIdAsync(int userId);
        Task<agencia> AddAsync(agencia agencia);
        Task<agencia?> UpdateAsync(agencia agencia);
        Task<bool> DeleteAsync(int id);
    }
}
