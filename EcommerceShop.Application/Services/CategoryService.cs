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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISUbCategoryRepository subCategoryRepository;
        private readonly IModuleRepository moduleRepository;
        private readonly IFileServcie fileServcie;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository,ISUbCategoryRepository subCategoryRepository,IModuleRepository moduleRepository, IFileServcie fileServcie,IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.moduleRepository = moduleRepository;
            this.fileServcie = fileServcie;
            this.mapper = mapper;
        }



        public async Task<APIResponse<CategoryResponse>> AddCategory(CategoryRequest model)
        {
            var isCategoryAdded = await categoryRepository.IsExistsAsync(_ => _.Name == model.Name);
            if (isCategoryAdded) return APIResponse<CategoryResponse>.ErrorResponse(AppMessage.AlreadyCreated);

            var category = mapper.Map<Category>(model);

            var res = await categoryRepository.AddAsync(category);
            if (res > 0) return APIResponse<CategoryResponse>.SuccessResponse(mapper.Map<CategoryResponse>(category),message:AppMessage.Created);

            return APIResponse<CategoryResponse>.ErrorResponse(AppMessage.InternalServerError);
        }



        public async Task<APIResponse<SubCategoryResponse>> AddSubCategory(SubCategoryRequest model)
        {
            var isSubCategoryAdded = await subCategoryRepository.IsExistsAsync(_ => _.Name == model.Name);
            if (isSubCategoryAdded) return APIResponse<SubCategoryResponse>.ErrorResponse(AppMessage.AlreadyCreated);

            var subCategory = mapper.Map<SubCategory>(model);
            var module = await moduleRepository.FirstOrDefaultAsync(_ => _.ModuleName == "category");
            var file =  await fileServcie.UploadFile(model.File,module is not null? module.Id : Guid.Empty, subCategory.Id);

            if (file is not null) subCategory.FilePath = file!.Result!.FilePath;

            var res = await subCategoryRepository.AddAsync(subCategory);

            if (res > 0) return APIResponse<SubCategoryResponse>.SuccessResponse(mapper.Map<SubCategoryResponse>(subCategory),message: AppMessage.Created);

            return APIResponse<SubCategoryResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            if (!categories.Any()) APIResponse<IEnumerable<CategoryResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<CategoryResponse>>.SuccessResponse(mapper.Map<IEnumerable<CategoryResponse>>(categories));
        }




        public async Task<APIResponse<IEnumerable<SubCategoryResponse>>> GetAllSubCategories(Guid categoryId)
        {
            var subCategories = await subCategoryRepository.FilterAsync(_=>_.CategoryId == categoryId);

            if (!subCategories.Any()) APIResponse<IEnumerable<SubCategoryResponse>>.ErrorResponse();

            return APIResponse<IEnumerable<SubCategoryResponse>>.SuccessResponse(mapper.Map<IEnumerable<SubCategoryResponse>>(subCategories));
        }




        public async Task<APIResponse<CategoryResponse>> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null) return APIResponse<CategoryResponse>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<CategoryResponse>.SuccessResponse(mapper.Map<CategoryResponse>(category));
        }




        public async Task<APIResponse<SubCategoryResponse>> GetSubCategoryById(Guid id)
        {
            var subCategory = await subCategoryRepository.GetByIdAsync(id);

            if (subCategory is null) return APIResponse<SubCategoryResponse>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<SubCategoryResponse>.SuccessResponse(mapper.Map<SubCategoryResponse>(subCategory));
        }
    }
}
