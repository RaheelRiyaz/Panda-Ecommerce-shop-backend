using AutoMapper;
using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Application.Utilis;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IStateRepository stateRepository;
        private readonly IMapper mapper;

        public CountryService(ICountryRepository countryRepository, IStateRepository stateRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.stateRepository = stateRepository;
            this.mapper = mapper;
        }



        public async Task<APIResponse<CountryResponse>> AddCountry(CountryRequest model)
        {
            var isCountryAdded = await countryRepository.IsExistsAsync(_ => _.Name == model.Name);
            if (isCountryAdded) return APIResponse<CountryResponse>.ErrorResponse(AppMessage.AlreadyCreated);

            var country = mapper.Map<Country>(model);
            var res = await countryRepository.AddAsync(country);

            if (res > 0) return APIResponse<CountryResponse>.SuccessResponse(mapper.Map<CountryResponse>(country));

            return APIResponse<CountryResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<StateResponse>> AddState(StateRequest model)
        {
            var isStateAdded = await stateRepository.IsExistsAsync(_=>_.Name== model.Name);

            if(isStateAdded) return APIResponse<StateResponse>.ErrorResponse(AppMessage.AlreadyCreated);

            var state = mapper.Map<State>(model);
            var res = await stateRepository.AddAsync(state);

            if (res > 0) return APIResponse<StateResponse>.SuccessResponse(mapper.Map<StateResponse>(state));

            return APIResponse<StateResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<IEnumerable<StateResponse>>> AddStates(IEnumerable<StateRequest> models)
        {
            foreach (var state in models)
            {
                var isStateAdded = await stateRepository.IsExistsAsync(_ => _.Name == state.Name);

                if (isStateAdded) return APIResponse<IEnumerable<StateResponse>>.ErrorResponse(AppMessage.AlreadyCreated);
            }

            var states = mapper.Map<List<State>>(models);

            var res = await stateRepository.AddRangeAsync(states);
            if(res > 0) return APIResponse<IEnumerable<StateResponse>>.SuccessResponse(mapper.Map<IEnumerable<StateResponse>>(states));

            return APIResponse<IEnumerable<StateResponse>>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<IEnumerable<StateResponse>>> GetAllStatesByCountry(Guid countryId)
        {
            var states = await stateRepository.FilterAsync(_ => _.CountryId == countryId);

            if (!states.Any()) return APIResponse<IEnumerable<StateResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<StateResponse>>.SuccessResponse(mapper.Map<IEnumerable<StateResponse>>(states));
        }




        public async Task<APIResponse<IEnumerable<CountryResponse>>> GetCountries()
        {
            var countries = await countryRepository.GetAllAsync();

            if (!countries.Any()) return APIResponse<IEnumerable<CountryResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<CountryResponse>>.SuccessResponse(mapper.Map<IEnumerable<CountryResponse>>(countries));
        }




        public async Task<APIResponse<IEnumerable<CountryResponse>>> SearchCountries(string term)
        {
            var countries = await countryRepository.FilterAsync(_ => _.Name.StartsWith(term));
            if (!countries.Any()) return APIResponse<IEnumerable<CountryResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<CountryResponse>>.SuccessResponse(mapper.Map<IEnumerable<CountryResponse>>(countries));
        }




        public async Task<APIResponse<IEnumerable<StateResponse>>> SearchStates(string term)
        {
            var countries = await stateRepository.FilterAsync(_ => _.Name.StartsWith(term));
            if (!countries.Any()) return APIResponse<IEnumerable<StateResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<StateResponse>>.SuccessResponse(mapper.Map<IEnumerable<StateResponse>>(countries));
    }
    }
}
