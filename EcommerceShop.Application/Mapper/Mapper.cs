using AutoMapper;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserSignupRequest, User>();
            CreateMap<User, UserSignupResponse>();
            CreateMap<User, UserDetails>();
        }

        public class CategoryMapper : Profile
        {
            public CategoryMapper()
            {
                CreateMap<CategoryRequest, Category>();
                CreateMap<Category, CategoryResponse>();
                CreateMap<SubCategoryRequest, SubCategory>();
                CreateMap<SubCategory, SubCategoryResponse>();
            }
        }


        public class ModuleMapper : Profile
        {
            public ModuleMapper()
            {
                CreateMap<ModuleRequest, Module>();
                CreateMap<Module, ModuleResponse>();
            }
        }


        public class FileMapper : Profile
        {
            public FileMapper()
            {
                CreateMap<AppFile, AppFileResponse>();
            }
        }

        public class ProductMapper : Profile
        {
            public ProductMapper()
            {
                CreateMap<ProductRequest, Product>();
                CreateMap<Product, ProductResponse>();
                CreateMap<Product, ProductItem>();
            }
        }

        public class ProductSizeMapper : Profile
        {
            public ProductSizeMapper()
            {
                CreateMap<ProductSizeRequest, ProductSizeChart>();
                CreateMap<ProductSizeChart, ProductSizeResponse>();
            }
        }


        public class CartMapper : Profile
        {
            public CartMapper()
            {
                CreateMap<CartRequest, Cart>();
                CreateMap<CartResponse, UserCartResponse>();
            }
        }

        public class CountryMapper : Profile
        {
            public CountryMapper()
            {
                CreateMap<Country, CountryResponse>();
                CreateMap<State, StateResponse>();
                CreateMap<CountryRequest, Country>();
                CreateMap<StateRequest, State>();
            }
        }


        public class AddressMapper : Profile
        {
            public AddressMapper()
            {
                CreateMap<Address, AddressResponse>();
                CreateMap<AddressRequest, Address>();
            }
        }


        public class OrderMapper : Profile
        {
            public OrderMapper()
            {
                CreateMap<OrderRequest, Order>();
                CreateMap<Order, OrderResponse>();
            }
        }

    }
}
