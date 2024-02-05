using EcommerceShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IContextService
    {
        public Guid GetUserId();
        public string GetUserName();
        public UserRole? GetUserRole();
    }
}
