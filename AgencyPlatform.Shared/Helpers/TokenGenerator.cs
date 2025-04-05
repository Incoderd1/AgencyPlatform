using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Shared.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
