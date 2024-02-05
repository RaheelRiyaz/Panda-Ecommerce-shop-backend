using EcommerceShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class UserSignupRequest
    {
        [Required(ErrorMessage = $"{nameof(UserName)} is required")]
        public string UserName { get; set; } = null!;


        [Required(ErrorMessage = $"{nameof(Email)} is required")]
        [RegularExpression(pattern: "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email is invalid")]
        [EmailAddress]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = $"{nameof(Password)} is required")]
        public string Password { get; set; } = null!;
    }

    public class UserSignupResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string UsernameOrEmail { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public UserRole UserRole { get; set; } 
    }

    public class ChangePasswordRequestModel
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }

    public class AdminSignupRequst : UserSignupRequest
    {
        public UserRole UserRole { get; set; }
    }


    public class RecoveryOptions
    {
        public string Email { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
    }

    public class UserDetails
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class UserAddress
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string LandMark { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public int Pincode { get; set; }
        public string HouseNo { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = null!;
    }

}
