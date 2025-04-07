using AgencyPlatform.Application.DTOs.MembresiasVip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services.MembresiasVip
{
    public interface IMembresiaVipService
    {
        Task<MembresiaVipDto> CrearAsync(CrearMembresiaVipDto dto);
        Task<MembresiaVipDto> ActualizarAsync(int idPlan, UpdateMembresiaVipDto dto);
        Task<bool> EliminarAsync(int idPlan);
        Task<MembresiaVipDto> ObtenerPorIdAsync(int idPlan);
        Task<List<MembresiaVipDto>> ObtenerTodasAsync();
    }
}
