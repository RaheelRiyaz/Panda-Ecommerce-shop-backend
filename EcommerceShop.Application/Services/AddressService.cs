using AutoMapper;
using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Application.Utilis;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IContextService contextService;

        public AddressService(IAddressRepository addressRepository,IMapper mapper, IContextService contextService)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.contextService = contextService;
        }



        public async Task<APIResponse<AddressResponse>> AddAddress(AddressRequest model)
        {
            var isAnyAddress = await addressRepository.IsExistsAsync(_=>_.EntityId == contextService.GetUserId());

            var address = mapper.Map<Address>(model);

            if (!isAnyAddress) address.IsDefault = true;

            if (isAnyAddress && (bool)model.IsDefault!)
            {
                var result = await addressRepository.RemoveDefaultAddress(contextService.GetUserId());
                if (result <= 0) return APIResponse<AddressResponse>.ErrorResponse(AppMessage.InternalServerError);

            }

            address.EntityId = contextService.GetUserId();

            var res = await addressRepository.AddAsync(address);

            if (res > 0) return APIResponse<AddressResponse>.SuccessResponse(mapper.Map<AddressResponse>(address),message:AppMessage.Created);

            return APIResponse<AddressResponse>.ErrorResponse(AppMessage.InternalServerError);
        }

        public async Task<APIResponse<string>> DeleteAddress(Guid addressId)
        {
            var res = await addressRepository.DeleteAddress(addressId);

            if (res > 0) return APIResponse<string>.SuccessResponse("Address Deleted Successfully");

            return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);
        }

        public async Task<APIResponse<AddressResponse>> GetAddressById(Guid id)
        {
            var address = await addressRepository.GetByIdAsync(id);

            if(address is null) return APIResponse<AddressResponse>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<AddressResponse>.SuccessResponse(mapper.Map<AddressResponse>(address));
        }




        public async Task<APIResponse<IEnumerable<CompactAddress>>> GetAllAddressesByEntity(Guid? entityId)
        {
            var id = contextService.GetUserId() == Guid.Empty ? entityId : contextService.GetUserId();

            var addresses = await addressRepository.GetCompactAddressesByEntity((Guid)id!);

            if (!addresses.Any()) return APIResponse<IEnumerable<CompactAddress>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<CompactAddress>>.SuccessResponse(addresses);
        }

        public async Task<APIResponse<string>> MakeAddressAsDefault(Guid addressId)
        {
            var removedefault = await addressRepository.RemoveDefaultAddress(contextService.GetUserId());
            if (removedefault <= 0) return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);

            var result = await addressRepository.MakeAddressAsDefault(addressId);
            if (result > 0) return APIResponse<string>.SuccessResponse("Address setted as default");

            return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);
        }
    }
}
