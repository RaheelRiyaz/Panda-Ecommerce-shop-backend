using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class AddressRequest
    {
        public int Pincode { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public string LandMark { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNo { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool? IsDefault { get; set; }
    }



    public class AddressResponse : Address
    {
         
    }

    public class CompactAddress
    {
        public Guid Id { get; set; }
        public string LandMark { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNo { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int Pincode { get; set; }
    }

    public class DefaultAddressRequest
    {
        public Guid AddressId { get; set; }
    }
}
