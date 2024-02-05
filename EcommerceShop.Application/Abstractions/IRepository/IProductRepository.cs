using EcommerceShop.Application.RRModels;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<ProductItem>> GetProducts();
        Task<IEnumerable<ProductItem>> GetProductsByMinAndMaxPrice(int min, int max, Guid subCategoryId);
        Task<IEnumerable<Suggestion>> GetSearchSuggestions(string term);
        Task<IEnumerable<ProductItem>> GetProductsByCategory(Guid categoryId);
        Task<IEnumerable<ProductItem>> GetProductsBySubCategory(Guid subCategoryId);
        Task<IEnumerable<ProductItem>> GetSimilarProducts(SimilarProductRequest model);
    }
}
