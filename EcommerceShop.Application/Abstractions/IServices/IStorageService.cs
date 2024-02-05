using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Abstractions.IServices
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> DeleteFileAsync(string filePath);
        Task<List<string>> DeleteFilesAsync(List<string> filePaths);
        Task<List<string>> UploadFilesAsync(IFormFileCollection files);
    }
}
