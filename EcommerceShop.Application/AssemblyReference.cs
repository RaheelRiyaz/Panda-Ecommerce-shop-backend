using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string rootPath)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<IStorageService>(new StorageService(rootPath));
            services.AddScoped<IFileServcie, FileService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IContextService, ContextService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
