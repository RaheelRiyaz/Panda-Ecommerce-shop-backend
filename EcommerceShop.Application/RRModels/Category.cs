using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } = null!;
    }
    public class CategoryResponse : CategoryRequest
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
    }

    public class SubCategoryRequest : CategoryRequest
    {
        public Guid CategoryId { get; set; }
        public IFormFile File { get; set; } = null!;
    }

    public class SubCategoryResponse
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
