using AgencyPlatform.Application.DTOs.Clientes;
using AgencyPlatform.Shared.Helpers;

namespace AgencyPlatform.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteDto> CreateAsync(int idUsuario, CrearClienteDto dto);
        Task<ClienteDto> UpdateAsync(int idCliente, UpdateClienteDto dto);
        Task<ClienteDto> GetByIdAsync(int idCliente);
        Task<ClienteResumenDto> GetResumenByIdAsync(int idCliente);
        Task<List<ClienteResumenDto>> GetAllAsync();
        Task<bool> DeleteAsync(int idCliente);

        Task<PaginatedResult<ClienteResumenDto>> GetPaginatedAsync(int page, int pageSize);


    }
}
