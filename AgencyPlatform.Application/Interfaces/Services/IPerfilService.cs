using AgencyPlatform.Application.DTOs.Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services
{
    public interface IPerfilService
    {
        Task<IEnumerable<PerfilDto>> GetAllAsync();
        Task<PerfilDto?> GetByIdAsync(int id);
        Task<PerfilDto> CreateAsync(CrearPerfilDto dto);
        Task<PerfilDto?> UpdateAsync(int id, UpdatePerfilDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
