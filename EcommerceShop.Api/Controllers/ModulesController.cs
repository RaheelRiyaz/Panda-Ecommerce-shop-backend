using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService moduleService;

        public ModulesController(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }




        [HttpGet]
        public async Task<APIResponse<IEnumerable<ModuleResponse>>> GetModules()
        {
            return await moduleService.GetAllModules();
        }





        [HttpGet("search/{term}")]
        public async Task<APIResponse<IEnumerable<ModuleResponse>>> SearchModules(string term)
        {
            return await moduleService.FilterModules(term);
        }





        [HttpGet("{id:guid}")]
        public async Task<APIResponse<ModuleResponse>> GetModuleById(Guid id)
        {
            return await moduleService.GetModuleById(id);
        }




        [HttpPost]
        public async Task<APIResponse<ModuleResponse>> AddModule(ModuleRequest model)
        {
            return await moduleService.AddModule(model);
        }
    }
}
