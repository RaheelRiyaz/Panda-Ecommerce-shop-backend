using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Domain.Models;
using EcommerceShop.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Persistence.Repository
{
    public class AppFileRepository : BaseRepository<AppFile>, IAppFileRepository
    {
        public AppFileRepository(EcommerceShopDbContext context) : base(context)
        {
        }
    }
}
