using BlogProject.Application.Catalog.Categories;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Post;
using BlogProject.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Post
{
   
    public class PostService : IPostService 
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostService(BlogDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public async Task<ApiResult<bool>> Create(PostRequest request, string userId)
        {
            if(request.Title == null) 
            {
                return new ApiErrorResult<bool>("Tiêu đề trống");
            }
            if (request.Desprition == null)
            {
                return new ApiErrorResult<bool>("Mô tả trống");
            }
            if (request.Content == null)
            {
                return new ApiErrorResult<bool>("Chưa nhập nội dung");
            }
           

            List<Posts> posts = await _context.Posts.Where(x=>x.Title == request.Title).ToListAsync();
            if (posts.Count != 0)
            {
                return new ApiErrorResult<bool>("Bài viết đã tồn tại");
            }
            // Lấy thông tin người dùng đăng nhập
            
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.FileImage.FileName;

            // Xác định đường dẫn tới thư mục lưu trữ hình ảnh trong thư mục gốc (wwwroot)
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            // Tạo đường dẫn đầy đủ tới tệp hình ảnh
            string imagePath = Path.Combine(uploadPath, uniqueFileName);

            // Lưu tệp hình ảnh
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                request.FileImage.CopyTo(stream);
            }
            var user = await _userManager.FindByIdAsync(userId);
            // Tạo một đối tượng Post từ dữ liệu PostViewModel
            var add = new Posts
            {

                Title = request.Title,
                Content = request.Content,
                UserId = user.Id, // Thiết lập UserId dựa trên thông tin đăng nhập
                UploadDate = DateTime.Now,
                Desprition = request.Desprition,
                Image = "/images/" + uniqueFileName,
            };
            
            
            _context.Posts.Add(add);
            await _context.SaveChangesAsync();
           
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int Id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(a=>a.PostID == Id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<PostVm>>> GetAll()
        {
            var posts = await _context.Posts.Include(p => p.User).ToListAsync();

            var postsWithUsernames = posts.Select(post => new PostVm
            {
                PostID = post.PostID,
                Name = post.User.UserName,
                Title = post.Title,
                Content = post.Content,
                UploadDate = post.UploadDate,
                View = post.View,
                Desprition = post.Desprition,

                
                
            }).ToList();
            return new ApiSuccessResult<List<PostVm>>(postsWithUsernames);
        }

        public Task<ApiResult<bool>> Update(PostRequest request,int Id)
        {
            throw new NotImplementedException();
        }
    }
}
