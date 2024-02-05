using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class Address : BaseEntity
    {
        public int Pincode { get; set; }
        public Guid EntityId { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public string LandMark { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNo { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool? IsDefault { get; set; }
        public bool IsDeleted { get; set; }




        #region Navigational Properties
        [ForeignKey(nameof(EntityId))]
        public User User { get; set; } = null!;


        [ForeignKey(nameof(StateId))]
        public State State { get; set; } = null!;
        #endregion Navigational Properties
    }
}
