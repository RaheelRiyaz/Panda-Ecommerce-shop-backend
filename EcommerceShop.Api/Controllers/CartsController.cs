using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Application.Utilis;
using EcommerceShop.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }





        [HttpPost]
        public async Task<APIResponse<CartRequest>> AddToCart(CartRequest model)
        {
            return await cartService.AddCart(model);
        }



        [HttpGet]

        public async Task<APIResponse<UserCartResponse>> GetUserCart()
        {
            if(User.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.UserRole)?.Value != UserRole.Customer.ToString())
            {
                throw new UnauthorizedAccessException();
            };
            return await cartService.UserCartItems();
        }




        [HttpGet("mycart-length")]
        public async Task<APIResponse<int>> CartItemsLength()
        {
            return await cartService.CartItemsLength();
        }




        [HttpGet("checkproductincart/{productId:guid}")]

        public async Task<APIResponse<bool>> CheckIsProductInUserCart(Guid productId)
        {
            return await cartService.CheckIsProductInUserCart(productId);
        }




        [HttpPut]
        public async Task<APIResponse<string>> UpdateCartQunatity(UpdateCartQuantityRequest model)
        {
            return await cartService.UpdateCartQuantity(model);
        }



        [HttpDelete("{productId:guid}")]
        public async Task<APIResponse<string>> RemoveCartItem(Guid productId)
        {
            return await cartService.RemoveItemFromCart(productId);
        }
    }
}
