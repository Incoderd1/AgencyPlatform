using AgencyPlatform.Application.DTOs.Cupones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services.Cupones
{
    public interface ICuponService
    {
        Task<List<CuponDto>> ObtenerDisponiblesAsync();
        Task<CuponDto?> ObtenerPorCodigoAsync(string codigo);
        Task CrearAsync(CrearCuponDto dto);
        Task<bool> CanjearCuponAsync(CanjearCuponDto dto);
    }
}
