using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class State : BaseEntity
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = null!;


        #region Navigational Properties
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } = null!;
        #endregion Navigational Properties
    }
}
