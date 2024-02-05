using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Utilis
{
   public static class AppMessage 
    {
        public const string InternalServerError = "There is some issue please try again later";

        public const string Created = "Created Sucessfully";
        public const string AlreadyCreated = "Already Created";

        public const string InvalidCredentials = "Username and or password is incorrect";

        public const string NotFound  = "The item you are looking for is not found";
    }
}
