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
    public class OrderService : IOrderService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IContextService contextService;
        private readonly IMapper mapper;
        private readonly IAddressRepository addressRepository;
        private readonly IProductSizeChartRepository productSizeChartRepository;

        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository, IContextService contextService, IMapper mapper, IAddressRepository addressRepository, IProductSizeChartRepository productSizeChartRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.contextService = contextService;
            this.mapper = mapper;
            this.addressRepository = addressRepository;
            this.productSizeChartRepository = productSizeChartRepository;
        }




        public async Task<APIResponse<OrderResponse>> GenerateOrder(OrderRequest model)
        {
            if (model.Quantity == 0) return APIResponse<OrderResponse>.ErrorResponse("Please Select atleast one item");

            var address = await addressRepository.FirstOrDefaultAsync(_ => _.EntityId == contextService.GetUserId() && _.IsDefault == true);
            if (address is null) return APIResponse<OrderResponse>.ErrorResponse("Please Add Your Address Before Placing Your Order");
            var product = await productRepository.GetByIdAsync(model.ProductId);
            if (product is null)
                return APIResponse<OrderResponse>.ErrorResponse("No Product Found");
            if (product.TotalStock < model.Quantity) return APIResponse<OrderResponse>.ErrorResponse($"{product.TotalStock} is available in our stock");
            /*
             product.TotalStock -= model.Quantity;
             var updateProductResponse = await productRepository.UpdateAsync(product);
             if (updateProductResponse == 0) return APIResponse<OrderResponse>.ErrorResponse(AppMessage.InternalServerError);*/

            var size = await productSizeChartRepository.FirstOrDefaultAsync(_ => _.Id == model.SizeId);
            if (size?.Quantity < model.Quantity) return APIResponse<OrderResponse>.ErrorResponse($"{size.Quantity} product is in the stock for the selected size");
            /*if(size is not null)
            {
                size.Quantity -= model.Quantity;
                var updateResponse = await productSizeChartRepository.UpdateAsync(size);
                if (updateResponse == 0) return APIResponse<OrderResponse>.ErrorResponse(AppMessage.InternalServerError);
            }*/

            var order = mapper.Map<Order>(model);
            order.EntityId = contextService.GetUserId();
            order.AddressId = address.Id;
            order.CreatedBy = contextService.GetUserId();

            var res = await orderRepository.AddAsync(order);

            if (res > 0) return APIResponse<OrderResponse>.SuccessResponse(mapper.Map<OrderResponse>(order));

            return APIResponse<OrderResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<OrderResponse>> CancelOrder(Guid orderId)
        {
            var order = await orderRepository.OrderByOrderId(orderId);

            if (order is null) return APIResponse<OrderResponse>.ErrorResponse(AppMessage.NotFound);

            if (order.OrderStatus == Domain.Enums.OrderStatus.Cancelled) return APIResponse<OrderResponse>.ErrorResponse("Order is already Cancelled");

            var res = await orderRepository.CancelOrder(orderId);

            if (res > 0) return APIResponse<OrderResponse>.SuccessResponse(order, message: "Order Cancelled Successfully");

            return APIResponse<OrderResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<OrderResponse>> DispatchOrder(Guid orderId)
        {
            var order = await orderRepository.OrderByOrderId(orderId);

            if (order is null) return APIResponse<OrderResponse>.ErrorResponse(AppMessage.NotFound);

            if (order.OrderStatus == Domain.Enums.OrderStatus.Dispatched) return APIResponse<OrderResponse>.ErrorResponse("Order has been already dispatched");

            var res = await orderRepository.DispatchOrder(orderId);
            if (res > 0) return APIResponse<OrderResponse>.SuccessResponse(order, message: "Order Dispatched");

            return APIResponse<OrderResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<OrderResponse>> OrderById(Guid orderId)
        {
            var order = await orderRepository.OrderByOrderId(orderId);

            if (order is null) return APIResponse<OrderResponse>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<OrderResponse>.SuccessResponse(order);
        }




        public async Task<APIResponse<OrderSummary>> OrderSummary(Guid orderId)
        {
            var orderSummary = await orderRepository.OderSummary(orderId);

            if (orderSummary is null) return APIResponse<OrderSummary>.ErrorResponse(AppMessage.InternalServerError);

            return APIResponse<OrderSummary>.SuccessResponse(orderSummary);
        }




        public async Task<APIResponse<IEnumerable<OrderResponse>>> ViewCustomerOrders(Guid? entityId)
        {
            var id = contextService.GetUserId();
            var orders = await orderRepository.ViewAllOrdersByEntity(entityId ?? contextService.GetUserId());

            if (!orders.Any()) return APIResponse<IEnumerable<OrderResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<OrderResponse>>.SuccessResponse(orders);
        }




        public async Task<APIResponse<IEnumerable<OrderResponse>>> ViewOrders()
        {
            var orders = await orderRepository.ViewAllOrders();
            if (!orders.Any()) return APIResponse<IEnumerable<OrderResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<OrderResponse>>.SuccessResponse(orders);
        }




        public async Task<APIResponse<IEnumerable<PendingOrderResponse>>> PendingOrders()
        {
            var orders = await orderRepository.PendingOrders();

            if (!orders.Any()) return APIResponse<IEnumerable<PendingOrderResponse>>.ErrorResponse("No Pending Order found");

            return APIResponse<IEnumerable<PendingOrderResponse>>.SuccessResponse(orders);
        }
    }
}
