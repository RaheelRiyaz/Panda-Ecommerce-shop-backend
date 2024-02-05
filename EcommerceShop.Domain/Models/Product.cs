using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class Product : BaseEntity
    {
        public string BrandName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public bool COD { get; set; }
        public int TotalStock { get; set; }
        public int Off { get; set; } = 0;

        #region Navigational Properties
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory? SubCategory { get; set;}
        #endregion Navigational Properties
    }
}
