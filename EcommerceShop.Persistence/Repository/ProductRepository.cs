using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Domain.Models;
using EcommerceShop.Persistence.Dapper;
using EcommerceShop.Persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {      
        private const string  baseQuery  = $@"
                                WITH RankedFiles AS(
                         SELECT
                             P.Id ,
                             A.FilePath,
                             P.[BrandName],
                             P.Price,
                             P.Description,
                             P.ProductName,
                             P.SubCategoryId,
                             ROW_NUMBER() OVER (PARTITION BY P.Id ORDER BY A.Id) AS RowNum
                            FROM
                                Products P
                            LEFT JOIN
                                AppFiles A ON P.Id = A.EntityId
                        )
                        SELECT
                                Id,
                                FilePath,
                                BrandName,
                                Price,
                                Description,
                                SubCategoryId,
                                ProductName
                        FROM
                            RankedFiles
                        WHERE
                            RowNum = 1  ";


        public ProductRepository(EcommerceShopDbContext context) : base(context)
        {

        }




        public async Task<IEnumerable<ProductItem>> GetProducts()
        {

            return await context.QueryAsync<ProductItem>(baseQuery);
        }

        
        
        
        public async Task<IEnumerable<ProductItem>> GetProductsByCategory(Guid categoryId)
        {
            string query = $@" 
                            WITH RankedFiles AS(
                        SELECT
                            P.Id ,
                            A.FilePath,
                            P.[BrandName],
                            P.Price,
                            P.ProductName,
                            P.Description,
                            ROW_NUMBER() OVER (PARTITION BY P.Id ORDER BY A.Id) AS RowNum
                        FROM
                            Products P
                        LEFT JOIN
                            AppFiles A ON P.Id = A.EntityId
                        WHERE
                            P.CategoryId = @categoryId
                    )
                    SELECT
                            Id,
                            FilePath,
                            BrandName,
                            Price,
                            ProductName,
                            Description
                    FROM
                        RankedFiles
                    WHERE
                        RowNum = 1;";

            return await context.QueryAsync<ProductItem>(query, new {categoryId});
        }




        public async Task<IEnumerable<ProductItem>> GetProductsByMinAndMaxPrice(int min, int max, Guid subcategoryId)
        {
            string query = $@"{baseQuery}  AND Price BETWEEN {min} AND {max} AND SubCategoryId = @subCategoryId";

            return await context.QueryAsync<ProductItem>(query, new {subcategoryId});
        }




        public async Task<IEnumerable<ProductItem>> GetProductsBySubCategory(Guid subCategoryId)
        {
            string query = $@" WITH RankedFiles AS(
                        SELECT
                            P.Id ,
                            A.FilePath,
                            P.[BrandName],
                            P.Price,
                            P.ProductName,
                            P.Description,
                            ROW_NUMBER() OVER (PARTITION BY P.Id ORDER BY A.Id) AS RowNum
                        FROM
                            Products P
                        LEFT JOIN
                            AppFiles A ON P.Id = A.EntityId
                        WHERE
                            P.SubCategoryId = @subCategoryId
                    )
                    SELECT
                            Id,
                            FilePath,
                            BrandName,
                            Price,
                            ProductName,
                            Description
                    FROM
                        RankedFiles
                    WHERE
                        RowNum = 1;";
            return await context.QueryAsync<ProductItem>(query, new { subCategoryId });

        }




        public async Task<IEnumerable<Suggestion>> GetSearchSuggestions(string term)
        {
            string query = $@"SELECT  TOP 20 
                            [Id],CategoryId,
                            SubCategoryId,
                            ProductName
                            FROM PRODUCTS
                            WHERE ProductName Like '%{term}%'";

            return await context.QueryAsync<Suggestion>(query);
        }




        public async Task<IEnumerable<ProductItem>> GetSimilarProducts(SimilarProductRequest model)
        {
            string query = $@"WITH RankedFiles AS(
                         SELECT
                             P.Id ,
                             A.FilePath,
                             P.[BrandName],
                             P.Price,
							 p.SubCategoryId,
                             P.Description,
                             ROW_NUMBER() OVER (PARTITION BY P.Id ORDER BY A.Id) AS RowNum
                            FROM
                                Products P
                            LEFT JOIN
                                AppFiles A ON P.Id = A.EntityId
                        )
                        SELECT
                                Id,
                                FilePath,
                                BrandName,
                                Price,
                                Description,
								SubCategoryId
                        FROM
                            RankedFiles
                        WHERE
                            RowNum = 1 AND SubCategoryId = @subCategoryId AND Id != @productId ";

            return await context.QueryAsync<ProductItem>(query,new { subCategoryId = model.SubCategoryId, productId = model.ProductId});
        }
    }
}
