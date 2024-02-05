using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IProductService 
    {
        Task<APIResponse<IEnumerable<ProductItem>>> GetProductsAsync();
        Task<APIResponse<IEnumerable<ProductItem>>> GetProductsByMinAndMaxPrice(FilterByPriceRangeRequest model);
        Task<APIResponse<IEnumerable<Suggestion>>> GetProductSuggestions(string term);
        Task<APIResponse<IEnumerable<ProductItem>>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<APIResponse<IEnumerable<ProductItem>>> GetProductsBySubCategoryIdAsync(Guid subCategoryId);
        Task<APIResponse<ProductResponse>> GetProductsByIdAsync(Guid id);
        Task<APIResponse<ProductResponse>> AddProduct(ProductRequest model);
        Task<APIResponse<IEnumerable<ProductItem>>> GetSimilarProducts(SimilarProductRequest model);
    }
}
