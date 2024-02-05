using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IRepository
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<IEnumerable<CartResponse>> GetUserCartItems(Guid entityId);

        Task<int> UpdateCartQuantity(UpdateCartQuantityRequest model);
        Task<int> RemoveItemFromCart(Guid cartId);
        Task<int?> CheckUserCartLength(Guid entityId);
    }
}
