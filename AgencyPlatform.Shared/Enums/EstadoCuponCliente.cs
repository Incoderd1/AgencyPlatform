using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Shared.Enums
{
    public enum EstadoCuponCliente
    {
        disponible,  // minúsculas para coincidir con PostgreSQL
        usado,
        expirado,
        cancelado
    }

}
