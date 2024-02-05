using EcommerceShop.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly string webrootPath;
        public StorageService(string webrootPath)
        {
            this.webrootPath = webrootPath;
        }





        public async Task<string> DeleteFileAsync(string filePath)
        {
            await Task.Run(() =>
            {
                File.Delete(webrootPath + filePath);
            });

            return filePath;
        }




        public async Task<List<string>> DeleteFilesAsync(List<string> filePaths)
        {
            List<string> fileResponses = new List<string>();

            await Task.Run(async () =>
            {
                foreach (var file in filePaths)
                {
                    fileResponses.Add(await DeleteFileAsync(file));
                }
            });

            return fileResponses;
        }




        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string path = string.Concat(Guid.NewGuid(), file.FileName);

            FileStream fileStream = new FileStream(GetPhysicalPath() + path, FileMode.Create);

            await file.CopyToAsync(fileStream);
            return GetVirtualPath() + path;
        }




        public async Task<List<string>> UploadFilesAsync(IFormFileCollection files)
        {
            List<string> fileResponses = new List<string>();

            await Task.Run(async () =>
            {
                foreach (var file in files)
                {
                    fileResponses.Add(await UploadFileAsync(file));
                }
            });

            return fileResponses;
        }



        #region Helper Functions
        private string GetPhysicalPath() => webrootPath + "/files/";
        private string GetVirtualPath() => "/files/";
        #endregion Helper Functions
    }
}
