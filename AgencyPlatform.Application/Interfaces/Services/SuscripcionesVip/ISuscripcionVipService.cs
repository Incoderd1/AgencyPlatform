using AgencyPlatform.Application.DTOs.SuscripcionesVip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services.SuscripcionesVip
{
    public interface ISuscripcionVipService
    {
        Task<List<SuscripcionVipDto>> ObtenerTodasAsync();
        Task<SuscripcionVipDto?> ObtenerPorIdAsync(int id);
        Task<SuscripcionVipDto> CrearAsync(CrearSuscripcionVipDto dto);
        Task<SuscripcionVipDto?> ActualizarAsync(int id, UpdateSuscripcionVipDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
