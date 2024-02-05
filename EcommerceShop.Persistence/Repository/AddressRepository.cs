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
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(EcommerceShopDbContext context) : base(context)
        {
            
        }

        public Task<int> DeleteAddress(Guid addressId)
        {
            string query = $@"UPDATE Addresses SET IsDeleted  = 1 WHERE Id = @addressId";

            return context.ExecuteAsync(query, new { addressId });
        }




        public async Task<IEnumerable<CompactAddress>> GetCompactAddressesByEntity(Guid entityId)
        {
            string query = $@"SELECT
                            C.Name as Country,
                            S.Name as State,
                            A.Pincode,
                            A.LandMark,
                            A.Street,
                            A.City,
                            A.HouseNo,
                            A.FullName,
                            A.ContactNo,
                            A.IsDefault,
                            A.Id 
                            from Addresses A INNER JOIN Countries C
                            ON C.Id = A.CountryId INNER JOIN States S ON
                            S.Id =A.StateId
                            WHERE EntityId = @entityId AND IsDeleted = 0";

            return await context.QueryAsync<CompactAddress>(query, new { entityId });
        }




        public async Task<int> MakeAddressAsDefault( Guid id)
        {
            string query = $@"UPDATE Addresses SET IsDefault = 1 WHERE id = @id";

            return await context.ExecuteAsync(query, new {id});
        }



        public async Task<int> RemoveDefaultAddress(Guid entityId)
        {
            string query = $@"UPDATE Addresses SET IsDefault = 0 WHERE EntityId = @entityId";
            return await context.ExecuteAsync(query, new { entityId });
        }
    }
}
