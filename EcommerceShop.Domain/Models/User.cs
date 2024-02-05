using EcommerceShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string ResetCode { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
    }
}
