using BlogProject.Application.Catalog.Categories;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace BlogProject.Application.Catalog.Post
{

    public class PostService : IPostService
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;
        public PostService(BlogDbContext context, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
        }

        public async Task<ApiResult<bool>> Create(PostRequest request, string userId)
        {
            if (request.Title == null)
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


            List<Posts> posts = await _context.Posts.Where(x => x.Title == request.Title).ToListAsync();
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
                CategoryId = request.CategoryId,
                Image = "/images/" + uniqueFileName,
            };


            _context.Posts.Add(add);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int Id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(a => a.PostID == Id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<PostVm>>> GetAllPaging(GetUserPagingRequest request)
        {
            var query = from p in _context.Posts

                        select new {p};

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Title.Contains(request.Keyword));


            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PostVm()
                {
                    Desprition = x.p.Desprition,
                    Title = x.p.Title,
                    CategoryId = x.p.CategoryId,
                    Image = x.p.Image,
                    Content = x.p.Content,
                    Like = x.p.Like,
                    UploadDate =x.p.UploadDate,
                    View = x.p.View
                }).ToListAsync();

            
            var pagedResult = new PagedResult<PostVm>()
            {
                TotalRecord = totalRow,

                Items = data
            };
            return new ApiSuccessResult<PagedResult<PostVm>>(pagedResult);

        }

        public async Task<ApiResult<PostVm>> GetById(int postId)
        {
            var posts = await _context.Posts.Where(x => x.PostID == postId).FirstOrDefaultAsync();
            if (posts == null)
            {
                return new ApiErrorResult<PostVm>("Danh mục không tồn tại");
            }

            var post = new PostVm()
            {
                Desprition = posts.Desprition,
                Title = posts.Title,
                CategoryId = posts.CategoryId,
                Image = posts.Image,
                Content = posts.Content,
                Like = posts.Like,
                UploadDate = posts.UploadDate,
                View = posts.View
            };
            return new ApiSuccessResult<PostVm>(post);
        }

        public async Task<ApiResult<bool>> Update(PostUpdateRequest request, int id)
        {
            if (id == null)
            {
                return new ApiErrorResult<bool>("Lỗi cập nhập");
            }
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id);
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


            post.Desprition = request.Desprition;
            post.Title = request.Title;
            post.Content = request.Content;
            post.CategoryId = request.CategoryId;
            post.Image = "/images/" + uniqueFileName;

            _context.Posts.Update(post);
            _context.SaveChanges();
            return new ApiSuccessResult<bool>(true);
        }
    }
}