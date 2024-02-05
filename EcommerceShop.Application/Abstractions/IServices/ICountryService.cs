using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface ICountryService
    {
        Task<APIResponse<IEnumerable<CountryResponse>>> GetCountries();
        Task<APIResponse<IEnumerable<StateResponse>>> SearchStates(string term);
        Task<APIResponse<IEnumerable<CountryResponse>>> SearchCountries(string term);
        Task<APIResponse<CountryResponse>> AddCountry(CountryRequest model);
        Task<APIResponse<StateResponse>> AddState(StateRequest model);
        Task<APIResponse<IEnumerable<StateResponse>>> AddStates(IEnumerable<StateRequest> models);
        Task<APIResponse<IEnumerable<StateResponse>>> GetAllStatesByCountry(Guid countryId);
    }
}
