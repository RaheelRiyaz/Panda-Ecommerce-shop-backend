using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Persistence.Data;
using EcommerceShop.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContextPool<EcommerceShopDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(EcommerceShopDbContext)));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepoistory>();
            services.AddScoped<ISUbCategoryRepository, SUbCategoryRepoistory>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IAppFileRepository, AppFileRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductSizeChartRepository, ProductSizeChartRepository>();
            return services;
        }
    }
}
