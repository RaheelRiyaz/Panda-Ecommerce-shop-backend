using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Domain.Models;
using EcommerceShop.Persistence.Dapper;
using EcommerceShop.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(EcommerceShopDbContext context):base(context)
        {
        }




        public async Task<RecoveryOptions?> GetRecoveryOptions(string userName)
        {
            string query = $@"SELECT Email ,ContactNo
                                FROM Users U
                                LEFT JOIN Addresses A
                                ON A.EntityId = U.Id
                                WHERE UserName = @userName";

            return await context.FirstOrDefaultAsync<RecoveryOptions>(query, new { userName });
        }

      
    }
}
