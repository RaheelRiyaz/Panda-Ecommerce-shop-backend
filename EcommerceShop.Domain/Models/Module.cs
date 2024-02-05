using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class Module : BaseEntity
    {
        public string ModuleName { get; set; } = null!;
    }
}
