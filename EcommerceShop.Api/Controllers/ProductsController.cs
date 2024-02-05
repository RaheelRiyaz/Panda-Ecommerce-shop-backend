using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }




        [HttpGet]
        public async Task<APIResponse<IEnumerable<ProductItem>>> GetAllProducts()
        {
            return await productService.GetProductsAsync();
        }



        [HttpPost("filter-by-price")]
        public async Task<APIResponse<IEnumerable<ProductItem>>> GetProductsByPriceRange(FilterByPriceRangeRequest model)
        {
            return await productService.GetProductsByMinAndMaxPrice(model);
        }



        [HttpGet("suggestions/{term}")]
        public async Task<APIResponse<IEnumerable<Suggestion>>> GetSuggestions(string term)
        {
            return await productService.GetProductSuggestions(term);
        }





        [HttpGet("categoryId/{categoryId:guid}")]
        public async Task<APIResponse<IEnumerable<ProductItem>>> GetAllProductsByCategory(Guid categoryId)
        {
            return await productService.GetProductsByCategoryIdAsync(categoryId);
        }



        [HttpGet("subcategory/{subCategoryId:guid}")]
        public async Task<APIResponse<IEnumerable<ProductItem>>> GetAllProductsBySubCategory(Guid subCategoryId)
        {
            return await productService.GetProductsBySubCategoryIdAsync(subCategoryId);
        }




        [HttpGet("{id:guid}")]
        public async Task<APIResponse<ProductResponse>> GetCompactProductById(Guid id)
        {
            return await productService.GetProductsByIdAsync(id);
        }





        [HttpPost]
        public async Task<APIResponse<ProductResponse>> AddProduct([FromForm] ProductRequest model)
        {
            return await productService.AddProduct(model);
        }



        [HttpPost("similar-products")]
        public async Task<APIResponse<IEnumerable<ProductItem>>> GetSimilarProducts(SimilarProductRequest model)
        {
            return await productService.GetSimilarProducts(model);
        }
    }
}
