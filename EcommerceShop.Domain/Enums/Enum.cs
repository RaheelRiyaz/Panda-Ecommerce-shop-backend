using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Enums
{
    public enum UserRole
    {
        Admin = 1,
        Customer = 2,
        ProductManager = 3
    }

    public enum PaymentMethod : byte
    {
        Pending = 1,
        Cod = 2,
        Paid = 3,
    }

    public enum OrderStatus : byte
    {
        Pending = 1,
        Dispatched = 2,
        Cancelled = 3,
        Completed = 4
    }
}
