using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IModuleService
    {
        Task<APIResponse<IEnumerable<ModuleResponse>>> GetAllModules();
        Task<APIResponse<IEnumerable<ModuleResponse>>>FilterModules(string term);
        Task<APIResponse<ModuleResponse>> GetModuleById(Guid id);
        Task<APIResponse<ModuleResponse>> AddModule(ModuleRequest model);
    }
}
