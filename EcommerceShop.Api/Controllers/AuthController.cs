using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }




        [HttpPost("user-signup")]
        public async Task<APIResponse<UserSignupResponse>> UserSignup(UserSignupRequest model)
        {
            return await userService.UserSignup(model);
        }




        [HttpPost("admin-signup")]
        public async Task<APIResponse<UserSignupResponse>> AdminSignup(AdminSignupRequst model)
        {
            return await userService.AdminSignup(model);
        }




        [HttpPost("login")]
        public async Task<APIResponse<LoginResponse>> Login(LoginRequest model)
        {
            return await userService.Login(model);
        }




        [Authorize]
        [HttpPut("change-password")]
        public async Task<APIResponse<string>> Changepassword(ChangePasswordRequestModel model)
        {
            return await userService.ChangePassword(model);
        }




        [HttpPost("getrecovery-options")]
        public async Task<APIResponse<RecoveryOptions>> GetRecoveryOptions([FromBody] string userName)
        {
            return await userService.GetRecoverOptions(userName);
        }

        [Authorize]
        [HttpGet("details")]
        public async Task<APIResponse<UserDetails>> UserDetails()
        {
            return await userService.GetUserDetails();
        }


        [HttpPost("forgot-password")]
        public async Task<APIResponse<string>> ForgotPassword(ForgotPasswordRequest model)
        {
            return await userService.ForgotPassword(model.Email);
        }
    }
}
