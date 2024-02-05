using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface ICategoryService
    {
        Task<APIResponse<SubCategoryResponse>> AddSubCategory(SubCategoryRequest model);
        Task<APIResponse<CategoryResponse>> AddCategory(CategoryRequest model);
        Task<APIResponse<IEnumerable<CategoryResponse>>> GetAllCategories();
        Task<APIResponse<IEnumerable<SubCategoryResponse>>> GetAllSubCategories(Guid categoryId);
        Task<APIResponse<CategoryResponse>> GetCategoryById(Guid id);
        Task<APIResponse<SubCategoryResponse>> GetSubCategoryById(Guid id);
    }
}
