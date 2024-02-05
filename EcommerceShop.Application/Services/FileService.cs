using AutoMapper;
using EcommerceShop.Application.Abstractions.IRepository;
using EcommerceShop.Application.Abstractions.IServices;
using EcommerceShop.Application.RRModels;
using EcommerceShop.Application.Shared;
using EcommerceShop.Application.Utilis;
using EcommerceShop.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Services
{
    public class FileService : IFileServcie
    {
        private readonly IStorageService storageService;
        private readonly IAppFileRepository appFileRepository;
        private readonly IMapper mapper;

        public FileService(IStorageService storageService, IAppFileRepository appFileRepository, IMapper mapper)
        {
            this.storageService = storageService;
            this.appFileRepository = appFileRepository;
            this.mapper = mapper;
        }



        public async Task<APIResponse<AppFileResponse>> UploadFile(IFormFile file, Guid moduleId, Guid entityId)
        {
            var filePath = await storageService.UploadFileAsync(file);
            var appFile = new AppFile()
            {
                FilePath = filePath,
                IsVideo = FileValidator.IsVideo(filePath),
                ModuleId = moduleId,
                EntityId = entityId
            };

            var res = await appFileRepository.AddAsync(appFile);

            if (res > 0) return APIResponse<AppFileResponse>.SuccessResponse(mapper.Map<AppFileResponse>(appFile));

            return APIResponse<AppFileResponse>.ErrorResponse();
        }




        public async Task<APIResponse<List<AppFileResponse>>> UploadFiles(IFormFileCollection files, Guid moduleId, Guid entityId)
        {
            var filePaths = await storageService.UploadFilesAsync(files);

            var appFiles = new List<AppFile>();

            await Task.Run(() =>
            {
                foreach (var file in filePaths)
                {
                    appFiles.Add(new AppFile()
                    {
                        EntityId = entityId,
                        IsVideo = FileValidator.IsVideo(file),
                        ModuleId = moduleId,
                        FilePath = file
                    });
                }
            });
            var res = await appFileRepository.AddRangeAsync(appFiles);

            if (res > 0) return APIResponse<List<AppFileResponse>>.SuccessResponse(mapper.Map<List<AppFileResponse>>(appFiles));

            return APIResponse<List<AppFileResponse>>.ErrorResponse(AppMessage.InternalServerError);
        }
    }
}
