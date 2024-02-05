using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.RRModels;
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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly string baseQuery = $@"SELECT P.Id as ProductId,
                                                O.SizeId,
                                                S.Size,
                                                O.CreatedOn,
                                                O.Quantity,
                                                O.OrderStatus,
                                                O.PaymentMethod, 
                                                P.ProductName,
                                                O.EntityId,
                                                P.BrandName,
                                                U.UserName as CustomerName,
                                                ST.Name as State,
                                                A.Street,
                                                A.LandMark,
                                                A.ContactNo,
                                                A.FullName,
                                                A.City,
                                                A.HouseNo,
                                                P.Price * O.Quantity as SubTotal,
                                                CO.Name as Country
                                                FROM Orders O INNER JOIN Products P
                                                ON O.ProductId = P.Id INNER JOIN Addresses A
                                                ON A.Id = O.AddressId INNER JOIN Countries CO
                                                ON CO.Id = A.CountryId INNER JOIN States ST
                                                ON ST.Id = A.StateId INNER JOIN ProductSizeCharts S
                                                ON S.Id = O.SizeId INNER JOIN Users U On 
                                                U.Id = O.EntityId  ";


        public OrderRepository(EcommerceShopDbContext context) : base(context)
        {
        }




        public async Task<int> CancelOrder(Guid orderId)
        {
            string query = $@"UPDATE Orders SET OrderStatus = 3 WHERE Id = @orderId";

            return await context.ExecuteAsync(query, new { orderId });
        }




        public async Task<int> DispatchOrder(Guid orderId)
        {
            string query = $@"UPDATE Orders SET OrderStatus = 2 WHERE Id = @orderId AND OrderStatus = 1";

            return await context.ExecuteAsync(query, new { orderId });
        }




        public Task<OrderSummary?> OderSummary(Guid id)
        {
            string query = $@"SELECT A.FullName,
                            A.HouseNo,
                            O.Id as OrderId,
                            A.City,
                            A.Pincode,
                            C.Name as Country,
                            S.Name as State,
                            P.ProductName,
                            P.Price,
                            O.Quantity,
                            P.COD,
                            P.Price * O.Quantity as SubTotal,
                            A.Street,
                            A.Pincode,
                            A.LandMark,
                            P.Description,
                            P.Id as ProductId,
                            A.Id as AddressId
                            FROM Orders O
                            INNER JOIN Products P
                            ON P.Id = O.ProductId 
                            INNER JOIN Addresses A
                            ON A.EntityId = O.EntityId 
                            INNER JOIN Countries C
                            ON C.Id = A.CountryId
                            INNER JOIN States S
                            ON S.Id = A.StateId
                            WHERE A.IsDefault=1
                            AND O.Id = @id  ";

            return context.FirstOrDefaultAsync<OrderSummary>(query, new { id });
        }




        public async Task<OrderResponse?> OrderByOrderId(Guid orderId)
        {
            return await context.FirstOrDefaultAsync<OrderResponse>(baseQuery + " WHERE O.Id = @orderId", new { orderId });
        }




        public async Task<IEnumerable<PendingOrderResponse>> PendingOrders()
        {
            string query = $@"SELECT Id,EntityId,ProductId,AddressId,
                            Size,Quantity,PaymentMethod,CreatedBy,
                            SizeId
                            FROM Orders
                            WHERE OrderStatus = 1
                            AND CreatedOn <= DATEADD(HOUR, -1, GETDATE());
                            ";

            return await context.QueryAsync<PendingOrderResponse>(query);
        }




        public async Task<IEnumerable<OrderResponse>> ViewAllOrders()
        {
            return await context.QueryAsync<OrderResponse>(baseQuery);
        }




        public async Task<IEnumerable<OrderResponse>> ViewAllOrdersByEntity(Guid entityId)
        {
            return await context.QueryAsync<OrderResponse>(baseQuery+" WHERE U.Id = @entityId", new {entityId});

        }
    }
}
