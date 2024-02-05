using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
