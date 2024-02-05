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
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductSizeChartRepository productSizeChartRepository;
        private readonly IMapper mapper;
        private readonly IFileServcie fileServcie;
        private readonly IAppFileRepository appFileRepository;

        public ProductService(IProductRepository productRepository, IProductSizeChartRepository productSizeChartRepository, IMapper mapper, IFileServcie fileServcie, IAppFileRepository appFileRepository)
        {
            this.productRepository = productRepository;
            this.productSizeChartRepository = productSizeChartRepository;
            this.mapper = mapper;
            this.fileServcie = fileServcie;
            this.appFileRepository = appFileRepository;
        }




        public async Task<APIResponse<ProductResponse>> AddProduct(ProductRequest model)
        {
            var product = mapper.Map<Product>(model);
            var filesResponse = await fileServcie.UploadFiles(model.Files, model.ModuleId, product.Id);

            var productRes = await productRepository.AddAsync(product);
            var response = mapper.Map<ProductResponse>(product);
            response.Files = filesResponse.Result!;

            if (model.ProductSizes is null)
            {
                if (productRes < 0) 
                    return APIResponse<ProductResponse>.ErrorResponse(AppMessage.InternalServerError);
                return APIResponse<ProductResponse>.SuccessResponse(response);
            }

            var productSizes = mapper.Map<List<ProductSizeChart>>(model.ProductSizes);

            foreach (var size in productSizes)
            {
                size.ProductId = product.Id;
            }

            var productSizeResponses = await productSizeChartRepository.AddRangeAsync(productSizes);

            if (productSizeResponses > 0)
            {
                response.ProductSizes = mapper.Map<List<ProductSizeResponse>>(productSizes);
                return APIResponse<ProductResponse>.SuccessResponse(response);
            }

            return APIResponse<ProductResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<IEnumerable<ProductItem>>> GetProductsAsync()
        {
            var products = await productRepository.GetProducts();
            if (!products.Any()) 
                return APIResponse<IEnumerable<ProductItem>>.ErrorResponse(AppMessage.NotFound);
            return APIResponse<IEnumerable<ProductItem>>.SuccessResponse(products);
        }




        public async Task<APIResponse<IEnumerable<ProductItem>>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            var products = await productRepository.GetProductsByCategory(categoryId);
            if (!products.Any()) 
                return APIResponse<IEnumerable<ProductItem>>.ErrorResponse(AppMessage.NotFound);
            return APIResponse<IEnumerable<ProductItem>>.SuccessResponse(products);
        }




        public async Task<APIResponse<ProductResponse>> GetProductsByIdAsync(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) 
                return APIResponse<ProductResponse>.ErrorResponse(AppMessage.NotFound);

            var response = mapper.Map<ProductResponse>(product);
            var files = appFileRepository.FilterAsync(_ => _.EntityId == product.Id);

            if (files.Result.Any()) 
                response.Files = mapper.Map<List<AppFileResponse>>(files.Result);

            var productSizes = await productSizeChartRepository.FilterAsync(_ => _.ProductId == product.Id);

            if (productSizes.Any()) 
                response.ProductSizes = mapper.Map<List<ProductSizeResponse>>(productSizes.OrderByDescending(_=>_.Quantity));

            return APIResponse<ProductResponse>.SuccessResponse(response);
        }




        public async Task<APIResponse<IEnumerable<ProductItem>>> GetProductsByMinAndMaxPrice(FilterByPriceRangeRequest model)
        {
            var products = await productRepository.GetProductsByMinAndMaxPrice(model.Min,model.Max,model.SubCategoryId);

            if (!products.Any()) 
                return APIResponse<IEnumerable<ProductItem>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<ProductItem>>.SuccessResponse(products);
        }




        public async Task<APIResponse<IEnumerable<ProductItem>>> GetProductsBySubCategoryIdAsync(Guid subCategoryId)
        {
            var products = await productRepository.GetProductsBySubCategory(subCategoryId);

            if (!products.Any()) 
                return APIResponse<IEnumerable<ProductItem>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<ProductItem>>.SuccessResponse(products);
        }




        public async Task<APIResponse<IEnumerable<Suggestion>>> GetProductSuggestions(string term)
        {
            var suggestions = await productRepository.GetSearchSuggestions(term);

            if (!suggestions.Any())
                return APIResponse<IEnumerable<Suggestion>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<Suggestion>>.SuccessResponse(suggestions);
        }

        public async Task<APIResponse<IEnumerable<ProductItem>>> GetSimilarProducts(SimilarProductRequest model)
        {
            var products = await productRepository.GetSimilarProducts(model);

            if (!products.Any()) 
                return APIResponse<IEnumerable<ProductItem>>.ErrorResponse(AppMessage.NotFound);

            return APIResponse<IEnumerable<ProductItem>>.SuccessResponse(products);
        }
    }
}
