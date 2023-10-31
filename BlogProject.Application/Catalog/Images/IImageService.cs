using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Images
{
    public interface IImageService
    {
        Task<ApiResult<bool>> SaveImage(List<IFormFile> images, Posts posts);
        Task<ApiResult<bool>> UpdateImage(List<IFormFile> images, Posts posts);       
        Task<string> GetById(int ID);
    }
}
