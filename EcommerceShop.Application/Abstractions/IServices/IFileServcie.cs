using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IFileServcie
    {
        Task<APIResponse<AppFileResponse>> UploadFile(IFormFile file, Guid moduleId, Guid entityId);
        Task<APIResponse<List<AppFileResponse>>> UploadFiles(IFormFileCollection files, Guid moduleId, Guid entityId);
    }
}
