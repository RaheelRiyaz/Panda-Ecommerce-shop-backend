using AutoMapper;
using EcommerceShop.Application.Abstractions.IEmailService;
using EcommerceShop.Application.Abstractions.IEmailTemplateRenderer;
using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.EmailSetting;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Application.Utilis;
using EcommerceShop.Domain.Enums;
using EcommerceShop.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IJWTService jWTService;
        private readonly IContextService contextService;
        private readonly IEmailTemplateRenderer emailTemplateRenderer;
        private readonly IEmailService emailService;

        public UserService(IUserRepository userRepository, IMapper mapper, IJWTService jWTService, IContextService contextService, IEmailTemplateRenderer emailTemplateRenderer, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.jWTService = jWTService;
            this.contextService = contextService;
            this.emailTemplateRenderer = emailTemplateRenderer;
            this.emailService = emailService;
        }




        public async Task<APIResponse<string>> ChangePassword(ChangePasswordRequestModel model)
        {
            var user = await userRepository.GetByIdAsync(contextService.GetUserId());

            if (user is null) return APIResponse<string>.ErrorResponse(AppMessage.NotFound,HttpStatusCode.NotFound);

            var isOldPasswordCorrect = AppEncryption.ComparePassword(user.Password, user.Salt, model.OldPassword);

            if(!isOldPasswordCorrect) return APIResponse<string>.ErrorResponse("Invalid Credentials",HttpStatusCode.BadRequest);

            if (user.Password == AppEncryption.GenerateHashedPassword(user.Salt, model.NewPassword)) return APIResponse<string>.ErrorResponse("Old password and new password is same");

            user.Password = AppEncryption.GenerateHashedPassword(user.Salt, model.NewPassword);

            var res = await userRepository.UpdateAsync(user);
            if (res > 0) return APIResponse<string>.SuccessResponse("Password Changed Successfully");

            return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<LoginResponse>> Login(LoginRequest model)
        {
            var user = await userRepository.FirstOrDefaultAsync(_ => _.UserName == model.UsernameOrEmail || _.Email == model.UsernameOrEmail);
            if (user is null) return APIResponse<LoginResponse>.ErrorResponse(AppMessage.InvalidCredentials, statusCode: HttpStatusCode.NotFound);

            if (!AppEncryption.ComparePassword(user.Password, user.Salt, model.Password)) return APIResponse<LoginResponse>.ErrorResponse(AppMessage.InvalidCredentials);

            var response = new LoginResponse
            {
                Email = user.Email,
                UserRole = user.UserRole,
                Token = jWTService.GenerateToken(user)
            };

            return APIResponse<LoginResponse>.SuccessResponse(response);
        }




        public async Task<APIResponse<UserSignupResponse>> UserSignup(UserSignupRequest model)
        {
            var isUserNameOrEmailTaken = await userRepository.IsExistsAsync(_=>_.UserName == model.UserName || _.Email == model.Email);
            if (isUserNameOrEmailTaken) return APIResponse<UserSignupResponse>.ErrorResponse("Username or Email is already taken");

            var user = mapper.Map<User>(model);
            user.UserRole = UserRole.Customer;
            user.Salt = AppEncryption.GeerateSalat();
            user.Password = AppEncryption.GenerateHashedPassword(user.Salt, user.Password);

            var res = await userRepository.AddAsync(user);

            if (res > 0) return APIResponse<UserSignupResponse>.SuccessResponse(mapper.Map<UserSignupResponse>(user), message: AppMessage.Created);

            return APIResponse<UserSignupResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<UserSignupResponse>> AdminSignup(AdminSignupRequst model)
        {
            var isUserNameOrEmailTaken = await userRepository.IsExistsAsync(_ => _.UserName == model.UserName || _.Email == model.Email);
            if (isUserNameOrEmailTaken) return APIResponse<UserSignupResponse>.ErrorResponse("Username or Email is already taken");

            if (!Enum.IsDefined(typeof(UserRole), model.UserRole)) return APIResponse<UserSignupResponse>.ErrorResponse("Userrole is not valid");

            var user = mapper.Map<User>(model); 
            user.Salt = AppEncryption.GeerateSalat();
            user.Password = AppEncryption.GenerateHashedPassword(user.Salt, user.Password);
            user.UserRole = model.UserRole;
            var res = await userRepository.AddAsync(user);

            if (res > 0) return APIResponse<UserSignupResponse>.SuccessResponse(mapper.Map<UserSignupResponse>(user), message: AppMessage.Created);

            return APIResponse<UserSignupResponse>.ErrorResponse(AppMessage.InternalServerError);
        }




        public async Task<APIResponse<RecoveryOptions>> GetRecoverOptions(string userName)
        {
            var userExists = await userRepository.IsExistsAsync(_ => _.UserName == userName);

            if (!userExists) return APIResponse<RecoveryOptions>.ErrorResponse(AppMessage.NotFound);

            var options = await userRepository.GetRecoveryOptions(userName);

            if (options is null) return APIResponse<RecoveryOptions>.ErrorResponse(AppMessage.InternalServerError);

            options.Email = GetHidedEmail(options.Email);
            options.ContactNo = GetHidedContactNo(options.ContactNo);

            return APIResponse<RecoveryOptions>.SuccessResponse(options, message: "Here is your recovery options you can select one of these options to recover your password");
        }


        public async Task<APIResponse<UserDetails>> GetUserDetails()
        {
            var user = await userRepository.GetByIdAsync(contextService.GetUserId());
            if (user is null) return APIResponse<UserDetails>.ErrorResponse(AppMessage.NotFound);

            var userDetails = mapper.Map<UserDetails>(user);

            return APIResponse<UserDetails>.SuccessResponse(userDetails);
        }



        public async Task<APIResponse<string>> ForgotPassword(string email)
        {
            var user = await userRepository.FirstOrDefaultAsync(_ => _.Email == email);
            if(user is null) return APIResponse<string>.ErrorResponse(AppMessage.NotFound);

            var emailStting = new MailSetting()
            {
                To = new List<string>() { email },
                Body = await emailTemplateRenderer.RenderTemplateAsync("ResetPassword.cshtml", user),
                Subject = "Reset Password"
            };

           var res = await emailService.SendEmailAsync(emailStting);

            if (res) return APIResponse<string>.SuccessResponse("Reset Code has been send to your email");

            return APIResponse<string>.ErrorResponse(AppMessage.InternalServerError);
        }

        #region Helper Functions

        private string GetHidedEmail(string email)
        {
            var totalLength = email.Length;
            var stringBuilder = new StringBuilder(email.Substring(email.IndexOf("@") - 2));

            var stars = totalLength - stringBuilder.Length;
            var starsStringBuilder = new StringBuilder();
            for (var i = 0;i < stars; i++)
            {
                starsStringBuilder.Append("*");
            }
            stringBuilder.Insert(0, starsStringBuilder);

            return stringBuilder.ToString();
        }


        private string GetHidedContactNo(string contactNo)
        {
            if (contactNo is null) return "**********";

            var totalLength = contactNo.Length;
            var stringBuilder = new StringBuilder();
            for (var i = 0;i < totalLength - 2;i ++)
            {
                stringBuilder.Append("*");
            }

            stringBuilder.Append(contactNo[totalLength - 1]);
            stringBuilder.Append(contactNo[totalLength - 2]);

            return stringBuilder.ToString();
        }

       

        #endregion Helper Functions

    }
}
