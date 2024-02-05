using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IAddressService
    {
        Task<APIResponse<IEnumerable<CompactAddress>>> GetAllAddressesByEntity(Guid? entityId);
        Task<APIResponse<AddressResponse>> AddAddress(AddressRequest model);
        Task<APIResponse<AddressResponse>> GetAddressById(Guid id);
        Task<APIResponse<string>> MakeAddressAsDefault(Guid addressId);
        Task<APIResponse<string>> DeleteAddress(Guid addressId);
    }
}
