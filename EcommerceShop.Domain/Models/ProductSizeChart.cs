using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class ProductSizeChart : BaseEntity
    {
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ProductId { get; set; }

        #region Navigational Propeties
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        #endregion Navigational Propeties
    }
}
