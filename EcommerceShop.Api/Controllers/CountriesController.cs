using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }





        [HttpGet]
        public async Task<APIResponse<IEnumerable<CountryResponse>>> GetAllCountries()
        {
            return await countryService.GetCountries();
        }





        [HttpGet("states/{countryId:guid}")]
        public async Task<APIResponse<IEnumerable<StateResponse>>> GetAllStatesByCountry(Guid countryId)
        {
            return await countryService.GetAllStatesByCountry(countryId);
        }





        [HttpGet("search/country/{term}")]
        public async Task<APIResponse<IEnumerable<CountryResponse>>> SearchCountry(string term)
        {
            return await countryService.SearchCountries(term);
        }





        [HttpGet("search/states/{term}")]
        public async Task<APIResponse<IEnumerable<StateResponse>>> SearchStates(string term)
        {
            return await countryService.SearchStates(term);
        }




        [HttpPost("state")]
        public async Task<APIResponse<StateResponse>> AddState(StateRequest model)
        {
            return await countryService.AddState(model);
        }




        [HttpPost]
        public async Task<APIResponse<CountryResponse>> AddCountry(CountryRequest model)
        {
            return await countryService.AddCountry(model);
        }




        [HttpPost("add-states")]
        public async Task<APIResponse<IEnumerable<StateResponse>>> AddStates(IEnumerable<StateRequest> models)
        {
            return await countryService.AddStates(models);
        }
    }
}
