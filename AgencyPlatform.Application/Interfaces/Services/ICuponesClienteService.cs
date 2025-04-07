using AgencyPlatform.Application.DTOs.CuponesCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services
{
    public interface ICuponesClienteService
    {
        Task<int> CreateAsync(CrearCuponClienteDto dto);
        Task<bool> UpdateAsync(int id, UpdateCuponClienteDto dto);
        Task<bool> DeleteAsync(int id);
        Task<CuponClienteDto> GetByIdAsync(int id);
        Task<List<CuponClienteDto>> GetAllAsync();
    }
}
