using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }

    }
}
