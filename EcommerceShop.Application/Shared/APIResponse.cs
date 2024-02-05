using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Shared
{
    public class APIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public T? Result { get; set; }


        public static APIResponse<T> SuccessResponse(T? result, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "Success")
        {
            return new APIResponse<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode,
                Result = result
            };
        }

        public static APIResponse<T> ErrorResponse(string message = "Error", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new APIResponse<T>
            {
                IsSuccess = !true,
                Message = message,
                StatusCode = statusCode,
            };
        }
    }
}
