using EcommerceShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class OrderRequest
    {
        //public Guid? AddressId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
       // public PaymentMethod PaymentMethod { get; set; }
    }

    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ProductName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Landmark { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string HouseNo { get; set; } = null!;
        public string City { get; set; } = null!;
        public double SubTotal { get; set; }
        public DateTime CreatedOn { get; set; }
    }



    public class OrderSummary
    {
        public string FullName { get; set; } = null!;
        public string HouseNo { get; set; } = null!;
        public Guid OrderId { get; set; }
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public double Price { get; set; }
        public bool COD { get; set; }
        public double SubTotal { get; set; }
        public string Street { get; set; } = null!;
        public int Pincode { get; set; }
        public int Quantity { get; set; }
        public string LandMark { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Guid AddressId { get; set; }

    }

    public class PendingOrderResponse
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid AddressId { get; set; }
        public Guid SizeId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
