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
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<OrderResponse>> ViewAllOrders();
        Task<OrderResponse?> OrderByOrderId(Guid orderId);
        Task<int> DispatchOrder(Guid orderId);
        Task<IEnumerable<OrderResponse>> ViewAllOrdersByEntity(Guid entityId);
        Task<int> CancelOrder(Guid orderId);
        Task<OrderSummary?> OderSummary(Guid id);
        Task<IEnumerable<PendingOrderResponse>> PendingOrders();
    }
}
