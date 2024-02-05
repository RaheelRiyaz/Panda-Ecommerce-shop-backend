using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class CountryRequest
    {
        public string Name { get; set; } = null!;
    }


    public class CountryResponse : CountryRequest
    {
        public Guid Id { get; set; } 
    }


    public class StateRequest : CountryRequest
    {
        public Guid CountryId { get; set; }
    }


    public class StateResponse : CountryResponse
    {

    }
}
