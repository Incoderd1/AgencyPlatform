using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface ICuponRepository
    {
        Task<List<Cupone>> ObtenerActivosAsync(DateTime? fecha = null);
        Task<Cupone?> ObtenerPorIdAsync(int id);
        Task<Cupone?> ObtenerPorCodigoAsync(string codigo);
        Task CrearAsync(Cupone cupone);
        Task<bool> ValidarDisponibilidadAsync(int idCupon, int idCliente);
        Task IncrementarUsoAsync(int idCupon);
    }
}
