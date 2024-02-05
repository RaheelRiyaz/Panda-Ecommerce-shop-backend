using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface ICartService
    {
        Task<APIResponse<UserCartResponse>> UserCartItems();
        Task<APIResponse<CartRequest>> AddCart(CartRequest model);
        Task<APIResponse<string>> RemoveItemFromCart(Guid cartId);
        Task<APIResponse<string>> UpdateCartQuantity(UpdateCartQuantityRequest model);
        Task<APIResponse<bool>> CheckIsProductInUserCart(Guid ProductId);
        Task<APIResponse<int>> CartItemsLength();
    }
}
