using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Domain.Models;
using EcommerceShop.Persistence.Dapper;
using EcommerceShop.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Repository
{
    public class CartRepository : BaseRepository<Cart> ,ICartRepository
    {
        public CartRepository(EcommerceShopDbContext context) : base(context)
        {
            
        }




        public async Task<int?> CheckUserCartLength(Guid entityId)
        {
            string query = $@"SELECT SUM(Quantity) as CartItems FROM CARTS WHERE EntityId = @entityId";

            return await context.FirstOrDefaultAsync<int?>(query, new { entityId });
        }




        public async Task<IEnumerable<CartResponse>> GetUserCartItems(Guid entityId)
        {
            string query = $@"  SELECT P.BrandName,
                            P.Description,
                            P.Id as ProductId,
                            P.Price as ProductPrice,
                            C.Quantity ,C.SizeId,
                            C.Id,
							SZ.Size,
                            C.Quantity * P.Price as Total
                            FROM CARTS C
                            LEFT JOIN PRODUCTS P
                            ON P.ID = C.ProductId
							LEFT JOIN ProductSizeCharts SZ
							ON SZ.Id = C.SizeId
                            WHERE C.EntityId = @entityId";


            return await context.QueryAsync<CartResponse>(query, new { entityId });
        }




        public async Task<int> RemoveItemFromCart(Guid cartId)
        {
            string query = $@"delete Carts WHERE Id = @cartId";

            return await context.ExecuteAsync(query,new {cartId});
        }




        public async Task<int> UpdateCartQuantity(UpdateCartQuantityRequest model)
        {
            string query = $"UPDATE CARTS SET Quantity = @quantity WHERE EntityId = @entityId AND ProductId = @productId";

            return await context.ExecuteAsync(query, new { model.EntityId, model.Quantity, model.ProductId });
        }
    }
}
