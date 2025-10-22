using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public record CategoryDTO
    (string name,string description);
    public record UpdateCategoryDTO
    (int id,string name,string description);

}
