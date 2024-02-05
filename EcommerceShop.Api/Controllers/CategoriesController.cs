using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }




        [HttpGet]
        public async Task<APIResponse<IEnumerable<CategoryResponse>>> GetCategories()
        {
            return await categoryService.GetAllCategories();
        }





        [HttpGet("sub-categories/{categoryId:guid}")]
        public async Task<APIResponse<IEnumerable<SubCategoryResponse>>> GetSUbCategories(Guid categoryId)
        {
            return await categoryService.GetAllSubCategories(categoryId);
        }




        [HttpPost]
        public async Task<APIResponse<CategoryResponse>> AddCategory(CategoryRequest model)
        {
            return await categoryService.AddCategory(model);
        }




        [HttpPost("sub-category")]
        public async Task<APIResponse<SubCategoryResponse>> AddCategory([FromForm]SubCategoryRequest model)
        {
            return await categoryService.AddSubCategory(model);
        }




        [HttpGet("{id:guid}")]
        public async Task<APIResponse<CategoryResponse>> GetCategoryById(Guid id)
        {
            return await categoryService.GetCategoryById(id);
        }



        [HttpGet("sub-category/{id:guid}")]
        public async Task<APIResponse<SubCategoryResponse>> GetSubCategoryById(Guid id)
        {
            return await categoryService.GetSubCategoryById(id);
        }
    }
}
