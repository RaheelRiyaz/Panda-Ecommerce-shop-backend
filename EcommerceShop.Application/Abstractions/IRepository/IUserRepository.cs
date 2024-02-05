using EcommerceShop.Application.RRModels;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<RecoveryOptions?> GetRecoveryOptions(string userName);

    }
}
