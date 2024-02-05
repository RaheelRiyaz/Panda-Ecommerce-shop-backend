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
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly IContextService contextService;
        private readonly IAppFileRepository appFileRepository;

        public CartService(ICartRepository cartRepository, IMapper mapper, IContextService contextService,IAppFileRepository appFileRepository)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
            this.contextService = contextService;
            this.appFileRepository = appFileRepository;
        }



        public async Task<APIResponse<CartRequest>> AddCart(CartRequest model)
        {
            var isItemInCart = await cartRepository.FirstOrDefaultAsync(_ => _.EntityId == contextService.GetUserId() && _.ProductId == model.ProductId && _.SizeId == model.SizeId);

            if (isItemInCart is not null)
            {
                isItemInCart.Quantity += model.Quantity;
                var updateCartResponse = await cartRepository.UpdateAsync(isItemInCart);
                if (updateCartResponse > 0) return APIResponse<CartRequest>.SuccessResponse(model);
                return APIResponse<CartRequest>.ErrorResponse(message: AppMessage.InternalServerError);
            }

            var cart = mapper.Map<Cart>(model);
            cart.EntityId = contextService.GetUserId();

            var res = await cartRepository.AddAsync(cart);

            if (res > 0) return APIResponse<CartRequest>.SuccessResponse(model);

            return APIResponse<CartRequest>.ErrorResponse(message: AppMessage.InternalServerError);
        }




        public async Task<APIResponse<int>> CartItemsLength()
        {
            var length = await cartRepository.CheckUserCartLength(contextService.GetUserId());

            return APIResponse<int>.SuccessResponse(Convert.ToInt32(length));
        }




        public async Task<APIResponse<bool>> CheckIsProductInUserCart(Guid productId)
        {
            var isAdded = await cartRepository.IsExistsAsync(_ => _.EntityId == contextService.GetUserId() && _.ProductId == productId);

            return APIResponse<bool>.SuccessResponse(isAdded);
        }




        public async Task<APIResponse<string>> RemoveItemFromCart(Guid cartId)
        {
            var res = await cartRepository.RemoveItemFromCart(cartId);

            if (res > 0) return APIResponse<string>.SuccessResponse("Cart Item removed successfully");

            return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<string>> UpdateCartQuantity(UpdateCartQuantityRequest model)
        {
            model.EntityId = contextService.GetUserId();
            var res = await cartRepository.UpdateCartQuantity(model);
            if (res > 0) return APIResponse<string>.SuccessResponse("Cart Updated Successfully");

            return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<UserCartResponse>> UserCartItems()
        {
            var cartItems = await cartRepository.GetUserCartItems(contextService.GetUserId());

            if (!cartItems.Any()) return APIResponse<UserCartResponse>.ErrorResponse(AppMessage.NotFound);

            var subTotal = cartItems.Aggregate(0.0, (x, y) => x + y.Total);

            foreach (var product in cartItems)
            {
                var file = await appFileRepository.FirstOrDefaultAsync(_ => _.EntityId == product.ProductId);
                if (file is not null) product.FilePath = file.FilePath;
            }

            var userCartResponse = new UserCartResponse()
            {
                SubTotal = subTotal,
                CartItems = cartItems
            };

            return APIResponse<UserCartResponse>.SuccessResponse(userCartResponse);
        }
    }
}
