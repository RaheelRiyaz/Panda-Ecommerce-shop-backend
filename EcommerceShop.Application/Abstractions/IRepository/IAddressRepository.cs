using EcommerceShop.Application.RRModels;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IRepository
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<int> RemoveDefaultAddress(Guid entityId);

        Task<IEnumerable<CompactAddress>> GetCompactAddressesByEntity(Guid entityId);
        Task<int> MakeAddressAsDefault(Guid addressId);
        Task<int> DeleteAddress(Guid addressId);
    }
}
