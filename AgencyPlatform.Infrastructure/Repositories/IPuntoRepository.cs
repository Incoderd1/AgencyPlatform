using AgencyPlatform.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public interface IPuntoRepository
    {
        Task CrearAsync(Punto punto);
        Task<List<Punto>> ObtenerPorClienteAsync(int idCliente);
        Task<(int total, int usados, int expirados)> ObtenerResumenAsync(int idCliente);
    }
}
