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
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }




        [HttpPost("generate-order")]
        public async Task<APIResponse<OrderResponse>> GenerateOrder(OrderRequest model)
        {
            return await orderService.GenerateOrder(model);
        }





        [HttpDelete("cancel-order/{orderId:guid}")]
        public async Task<APIResponse<OrderResponse>> CancelOrder(Guid orderId)
        {
            return await orderService.CancelOrder(orderId);
        }




        [HttpGet("{orderId:guid}")]
        public Task<APIResponse<OrderResponse>> OrderById(Guid orderId)
        {
            throw new NotImplementedException();
        }





        [HttpGet("customer/{entityId:guid?}")]
        public async Task<APIResponse<IEnumerable<OrderResponse>>> ViewCustomerOrders(Guid? entityId)
        {
            return await orderService.ViewCustomerOrders(entityId);
        }





        [HttpGet("all-orders")]
        public async Task<APIResponse<IEnumerable<OrderResponse>>> ViewOrders()
        {
            return await orderService.ViewOrders();
        }




        [HttpPut("dispatch-order")]
        public async Task<APIResponse<OrderResponse>> DispatchOrder([FromBody] Guid orderId)
        {
            return await orderService.DispatchOrder(orderId);
        }




        [HttpGet("order-summary/{id:guid}")]
        public async Task<APIResponse<OrderSummary>> OrderSummary(Guid id)
        {
            return await orderService.OrderSummary(id);
        }



        [HttpGet("pending-orders")]
        public async Task<APIResponse<IEnumerable<PendingOrderResponse>>> PendingOrders()
        {
            return await orderService.PendingOrders();
        }
    }
}
