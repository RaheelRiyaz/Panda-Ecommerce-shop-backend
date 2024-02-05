using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class CartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid? SizeId { get; set; }
    }


    public class CartResponse : CartRequest
    {
        public string BrandName { get; set; } = null!;
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public string FilePath { get; set; } = string.Empty;
        public int ProductPrice { get; set; }
        public string Size { get; set; } = null!;
        public double Total { get; set; }
    }


    public class UserCartResponse
    {
        public double SubTotal { get; set; }
        public IEnumerable<CartResponse> CartItems { get; set; } = new List<CartResponse>();
    }

    public class UpdateCartQuantityRequest
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Guid EntityId { get; set; }
    }
}
