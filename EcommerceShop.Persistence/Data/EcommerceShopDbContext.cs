using EcommerceShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Data
{
    public class EcommerceShopDbContext : DbContext
    {
        public EcommerceShopDbContext(DbContextOptions options) : base(options) { }

        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSizeChart> ProductSizeCharts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        #endregion Tables
    }
}
