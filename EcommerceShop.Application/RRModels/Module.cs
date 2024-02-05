using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class ModuleRequest
    {
        public string ModuleName { get; set; } = null!;
    }

    public class ModuleResponse : ModuleRequest
    {
        public Guid Id { get; set; }
    }
}
