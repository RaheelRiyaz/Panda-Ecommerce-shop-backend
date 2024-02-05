using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public Guid CategoryId { get; set; }



        #region Navigational Properties
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        #endregion Navigational Properties
    }
}
