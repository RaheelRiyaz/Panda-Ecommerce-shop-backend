using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IUserService
    {
        Task<APIResponse<UserSignupResponse>> UserSignup(UserSignupRequest model);
        Task<APIResponse<LoginResponse>> Login(LoginRequest model);
        Task<APIResponse<UserSignupResponse>> AdminSignup(AdminSignupRequst model);
        Task<APIResponse<string>> ChangePassword(ChangePasswordRequestModel model);
        Task<APIResponse<RecoveryOptions>> GetRecoverOptions(string userName);
        Task<APIResponse<UserDetails>> GetUserDetails();
        Task<APIResponse<string>> ForgotPassword(string email);
    }
}
