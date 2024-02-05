using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.Utilis;
using EcommerceShop.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class ContextService : IContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }



        public Guid GetUserId()
        {
            var id = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.Id)?.Value;
            return id is not null ? Guid.Parse(id) : Guid.Empty;
        }




        public string GetUserName()
        {
            var userName = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.UserName)?.Value;
            return userName is not null ? userName :string.Empty;
        }




        public UserRole? GetUserRole()
        {
            var userRole = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.UserRole)?.Value;
            return userRole is not null ? (UserRole)Enum.Parse(typeof(UserRole), userRole) : null;
        }
    }
}
