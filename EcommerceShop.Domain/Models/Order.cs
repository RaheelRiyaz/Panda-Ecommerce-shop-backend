using EcommerceShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class Order : BaseEntity
    {
        public Guid EntityId { get; set; }
        public Guid ProductId { get; set; }
        public Guid AddressId { get; set; }
        public Guid? SizeId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
       // public TimeOnly TimeSpan { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;





        #region Navigational Properties

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; } = null!;
        #endregion Navigational Properties
    }
}
