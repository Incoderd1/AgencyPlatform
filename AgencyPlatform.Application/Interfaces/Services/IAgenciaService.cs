using AgencyPlatform.Application.DTOs.Agencia;

namespace AgencyPlatform.Application.Interfaces.Services
{
    public interface IAgenciaService
    {
        Task<IEnumerable<AgenciaDto>> GetAllAsync();
        Task<AgenciaDto?> GetByIdAsync(int id);
        Task<AgenciaDto?> GetByUserIdAsync(int userId);
        Task<AgenciaDto> CreateAsync(CrearAgenciaDto dto);
        Task<AgenciaDto?> UpdateAsync(int id, UpdateAgenciaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
