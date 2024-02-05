using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class ProductRequest 
    {
        public string BrandName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid ModuleId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public double Price { get; set; }
        public bool COD { get; set; }
        public int TotalStock { get; set; }
        public int Off { get; set; }
        public IFormFileCollection Files { get; set; } = null!;
        public IEnumerable<ProductSizeRequest>? ProductSizes { get; set; } 
    }



    public class ProductResponse 
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public string BrandName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public double Price { get; set; }
        public bool COD { get; set; }
        public int TotalStock { get; set; }
        public int Off { get; set; }
        public string Description { get; set; } = null!;
        public List<AppFileResponse> Files { get; set; } = null!;
        public IEnumerable<ProductSizeResponse> ProductSizes { get; set; } = null!;
    }

    public class ProductSizeRequest
    {
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class ProductSizeResponse : ProductSizeRequest
    {
        public Guid Id { get; set; }
        public bool SoldOut { get; set; }
    }

    public class ProductItem
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public string BrandName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string FilePath { get; set; } = null!;
    }

    public class Suggestion
    {
        public string ProductName { get; set; } = null!;
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }

    }

    public class SimilarProductRequest
    {
        public Guid SubCategoryId { get; set; }
        public Guid ProductId { get; set; }
    }

    public record FilterByPriceRangeRequest(int Min,int Max,Guid SubCategoryId);
}
