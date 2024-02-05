using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IOrderService
    {
        Task<APIResponse<OrderResponse>> GenerateOrder(OrderRequest model);
        Task<APIResponse<OrderResponse>> OrderById(Guid orderId);
        Task<APIResponse<OrderResponse>> CancelOrder(Guid orderId);
        Task<APIResponse<OrderResponse>> DispatchOrder(Guid orderId);
        Task<APIResponse<IEnumerable<OrderResponse>>> ViewOrders();
        Task<APIResponse<IEnumerable<OrderResponse>>> ViewCustomerOrders(Guid? entityId);
        Task<APIResponse<OrderSummary>> OrderSummary(Guid orderId);
        Task<APIResponse<IEnumerable<PendingOrderResponse>>> PendingOrders();
    }
}
