using BlogProject.Application.Common;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogProject.Application.Catalog.Images
{
    public class ImageService : IImageService
    {
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageService _storageService;
        private readonly BlogDbContext _blogDbContext;
        private const string USER_CONTENT_FOLDER_NAME = "Images";
        public ImageService(IWebHostEnvironment webHostEnvironment, BlogDbContext blogDbContext, IStorageService storageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _blogDbContext = blogDbContext;
            _storageService = storageService;
        }
        public async Task<string> GetById(int ID)
        {
            return await _blogDbContext.Images.Where(x => x.PostId == ID).Select(x => x.Path).FirstOrDefaultAsync();
        }

        public async Task<ApiResult<bool>> SaveImage(List<IFormFile> images, Posts posts)
        {
            var ImageSave = new List<Data.Entities.Image>();
            foreach (var item in images)
            {
                var GetImages = new Data.Entities.Image()
                {
                    PostId = posts.PostID,
                    FileName = item.FileName,
                    Path = await SaveFile(item)
                };
                ImageSave.Add(GetImages);
            }
            _blogDbContext.Images.AddRangeAsync(ImageSave);
            //   _context.Locations.Update(location);
            await _blogDbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();

        }

        public async Task<ApiResult<bool>> UpdateImage(List<IFormFile> images, Posts posts)
        {
            var ImageSave = new List<Data.Entities.Image>();
            var list_image = _blogDbContext.Images.Where(x => x.PostId == posts.PostID).ToList();

            // Xóa ảnh cũ
            if (list_image != null && list_image.Count > 0)
            {
                for (int i = 0; i < list_image.Count; i++)
                {
                    list_image[i].Path = list_image[i].Path.Remove(0, 30);
                    await _storageService.DeleteFileAsync(list_image[i].Path);
                }
                _blogDbContext.Images.RemoveRange(list_image);
                await _blogDbContext.SaveChangesAsync();
            }

            foreach (var item in images)
            {
                var GetImages = new Data.Entities.Image()
                {
                    PostId = posts.PostID,
                    FileName = item.FileName,
                    Path = await SaveFile(item)
                };
                ImageSave.Add(GetImages);
            }

            await _blogDbContext.Images.AddRangeAsync(ImageSave);
            await _blogDbContext.SaveChangesAsync();


            return new ApiSuccessResult<bool>();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "https://localhost:7204/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
