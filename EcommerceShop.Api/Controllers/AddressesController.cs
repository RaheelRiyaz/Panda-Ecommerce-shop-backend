using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }




        [HttpGet("all-addresses/{entityId:guid?}")]
        public async Task<APIResponse<IEnumerable<CompactAddress>>> GetAllAddressesByEntity(Guid ? entityId)
        {
            return await addressService.GetAllAddressesByEntity(entityId);
        }




        [Authorize]
        [HttpPost]
        public async Task<APIResponse<AddressResponse>> AddAddress(AddressRequest model)
        {
            return await addressService.AddAddress(model);
        }




        [HttpGet("{id:guid}")]
        public async Task<APIResponse<AddressResponse>> AddressById(Guid id)
        {
            return await addressService.GetAddressById(id);
        }

        [Authorize]
        [HttpPut("make-default")]
        public async Task<APIResponse<string>> MakeAddressAsDefault(DefaultAddressRequest model)
        {
            return await addressService.MakeAddressAsDefault(model.AddressId);
        }

        [Authorize]
        [HttpDelete("{addressid:guid}")]
        public async Task<APIResponse<string>> DeleteAddress(Guid addressid)
        {
            return await addressService.DeleteAddress(addressid);
        }
    }
}
