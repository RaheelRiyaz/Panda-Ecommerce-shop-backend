using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class Cart : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid EntityId { get; set; }
        public int Quantity { get; set; }
        public Guid? SizeId { get; set; }

        #region Navigational Properties
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(EntityId))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties

    }
}
