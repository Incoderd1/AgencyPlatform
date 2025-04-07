using AgencyPlatform.Infrastructure.Data.Entities;


namespace AgencyPlatform.Application.Interfaces.Repositories
{
    public interface IAgenciaRepository
    {
        Task<IEnumerable<Agencia>> GetAllAsync();
        Task<Agencia?> GetByIdAsync(int id);
        Task<Agencia?> GetByUserIdAsync(int userId);
        Task<Agencia> AddAsync(Agencia agencia);
        Task<Agencia?> UpdateAsync(Agencia agencia);
        Task<bool> DeleteAsync(int id);
    }
}
