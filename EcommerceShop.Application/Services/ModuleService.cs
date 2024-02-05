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
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository moduleRepository;
        private readonly IMapper mapper;

        public ModuleService(IModuleRepository moduleRepository, IMapper mapper)
        {
            this.moduleRepository = moduleRepository;
            this.mapper = mapper;
        }



        public async Task<APIResponse<ModuleResponse>> AddModule(ModuleRequest model)
        {
            var moduleExists = await moduleRepository.IsExistsAsync(_=>_.ModuleName == model.ModuleName);

            if (moduleExists) return APIResponse<ModuleResponse>.ErrorResponse(AppMessage.AlreadyCreated);

            var module = mapper.Map<Module>(model);
            var res = await moduleRepository.AddAsync(module);

            if (res > 0) return APIResponse<ModuleResponse>.SuccessResponse(mapper.Map<ModuleResponse>(module));

            return APIResponse<ModuleResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<IEnumerable<ModuleResponse>>> FilterModules(string term)
        {
            var modules = await moduleRepository.FilterAsync(_ => _.ModuleName.StartsWith(term));

            if (!modules.Any()) return APIResponse<IEnumerable<ModuleResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<ModuleResponse>>.SuccessResponse(mapper.Map<IEnumerable<ModuleResponse>>(modules));
        }




        public async Task<APIResponse<IEnumerable<ModuleResponse>>> GetAllModules()
        {
            var modules = await moduleRepository.GetAllAsync();

            if (!modules.Any()) return APIResponse<IEnumerable<ModuleResponse>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<ModuleResponse>>.SuccessResponse(mapper.Map<IEnumerable<ModuleResponse>>(modules));
        }




        public async Task<APIResponse<ModuleResponse>> GetModuleById(Guid id)
        {
            var module = await moduleRepository.GetByIdAsync(id);

            if (module is null) return APIResponse<ModuleResponse>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<ModuleResponse>.SuccessResponse(mapper.Map<ModuleResponse>(module));
        }
    }
}
