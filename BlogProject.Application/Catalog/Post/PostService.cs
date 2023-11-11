using BlogProject.Application.Catalog.Categories;
using BlogProject.Application.Catalog.Comments;
using BlogProject.Application.Catalog.Likes;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
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
using static System.Net.Mime.MediaTypeNames;

namespace BlogProject.Application.Catalog.Post
{

    public class PostService : IPostService
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
    /*    private readonly ICommentService _commentService;*/
        private readonly ILikeService _likeService;

        public PostService(BlogDbContext context, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment/*, ICommentService commentService*/, ILikeService likeService)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
           /* _commentService = commentService;*/
            _likeService = likeService;
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
           /* if (request.CategoryId == null || request.CategoryId <= 0)
            {
                return new ApiErrorResult<bool>("CategoryId không hợp lệ");
            }*/

            // Check if the specified CategoryId exists in the database
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoriesID == request.CategoryId);
            if (!categoryExists)
            {
                return new ApiErrorResult<bool>("CategoryId không tồn tại");
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

        public async Task<ApiResult<List<PostVm>>> GetAll()
        {
            var posts = await _context.Posts.Include(p => p.User).ToListAsync();

            var postsWithUsernames = posts.Select(post => new PostVm
            {
                PostID = post.PostID,
                UserName = post.User.UserName,
                Title = post.Title,
                Content = post.Content,
                UploadDate = post.UploadDate,
                View = post.View,
                CategoryId = post.CategoryId,
                Desprition = post.Desprition,
                Image = post.Image,
            }).ToList();
            return new ApiSuccessResult<List<PostVm>>(postsWithUsernames);
        }


        public async Task<ApiResult<List<PostVm>>> GetByUserId(string userId)
        {
            try
            {
                var posts = await _context.Posts
             .Where(p => p.UserId == Guid.Parse(userId))
             .Include(p => p.User)
             .ToListAsync();

                var postsWithUsernames = posts.Select(post => new PostVm
                {
                    PostID = post.PostID,
                    UserName = post.User.UserName,
                    Title = post.Title,
                    Content = post.Content,
                    UploadDate = post.UploadDate,
                    View = post.View,
                    CategoryId = post.CategoryId,
                    Desprition = post.Desprition,
                    Image = post.Image,
                }).ToList();

                return new  ApiSuccessResult<List<PostVm>>(postsWithUsernames);
            }
            catch (Exception ex)
            {
                return new  ApiErrorResult<List<PostVm>>($"An error occurred while retrieving posts by user ID: {ex.Message}");
            }
        }

        public async Task<ApiResult<List<PostVm>>> Search(string searchTerm)
        {
            try
            {
                var posts = await _context.Posts
                    .Where(p => p.Title.Contains(searchTerm) || p.Desprition.Contains(searchTerm))
                    .Include(p => p.User)
                    .ToListAsync();

                var postsWithUsernames = posts.Select(post => new PostVm
                {
                    PostID = post.PostID,
                    UserName = post.User.UserName,
                    Title = post.Title,
                    Content = post.Content,
                    UploadDate = post.UploadDate,
                    View = post.View,
                    CategoryId = post.CategoryId,
                    Desprition = post.Desprition,
                    Image = post.Image,
                }).ToList();

                return new ApiSuccessResult<List<PostVm>>(postsWithUsernames);
            }
            catch (Exception ex)
            {
                return new  ApiErrorResult<List<PostVm>>($"An error occurred while searching for posts: {ex.Message}");
            }
        }

        public async Task<PagedResult<PostVm>> GetPaged(GetUserPagingRequest request)
        {
            var query = from p in _context.Posts
                        select new { p };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Title.Contains(request.Keyword) || x.p.Desprition.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PostVm()
                {

                    Title = x.p.Title,
                    Content = x.p.Content,
                    UserName = x.p.User.UserName,
                    Desprition = x.p.Desprition,
                    Image = x.p.Image,
                    View = x.p.View,
                    UploadDate = x.p.UploadDate,
                    CategoryId = x.p.CategoryId,
                    PostID = x.p.PostID

                }).ToListAsync();
            //4. Select and projection
            var pagedResult = new PagedResult<PostVm>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
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

        public async  Task<List<PostVm>> TakeTopByQuantity(int quantity)
        {
            if (_context.Posts.Where(x => x.Active == Data.Enum.Active.no).ToList().Count < quantity) { quantity = _context.Posts.ToList().Count; }
            var post = await _context.Posts
            .OrderByDescending(p => p.View)
            .Take(quantity)
            .ToListAsync();

            List<PostVm> postList = new List<PostVm>();
            foreach (var item in post)
            {
                PostVm postVm = new PostVm();
                postVm.Title = item.Title;
                postVm.UserId = item.UserId;
                postVm.PostID = item.PostID;
                postVm.UploadDate = item.UploadDate;
                postVm.View = item.View;

                postList.Add(postVm);
            }

            return postList;
        }

        /*   public async Task<ApiResult<PagedResult<PostVm>>> GetPostFollowPaging(GetUserPagingRequest request)
           {
               try
               {
                   var userId = await _userService.GetIdByUserName(request.Name);

                   // Get the list of users the specified user is following
                   var followings = await _context.Follows
                       .Where(x => x.FolloweeId == userId)
                       .ToListAsync();

                   // Get all posts
                   var posts = await _context.Posts
                       .Where(post => followings.Any(user => user.FollowerId == post.UserId) && post.Active == 0)
                       .OrderByDescending(post => post.PostID)
                       .ToListAsync();

                   // Paging
                   int totalRow = posts.Count();

                   var data = posts
                       .Skip((request.PageIndex - 1) * request.PageSize)
                       .Take(request.PageSize)
                       .Select(post => new PostVm()
                       {
                           PostID = post.PostID,
                           Title = post.Title,
                           UploadDate = post.UploadDate,
                         *//*  CountComment = _commentService.CountById(post.PostID),*//*
                           CountLike = _likeService.CountById(post.PostID),
                           UserName = _userService.GetUserNameById(post.UserId),
                           View = post.View
                       })
                       .ToList();

                   // Select and projection
                   var pagedResult = new PagedResult<PostVm>()
                   {
                       TotalRecords = totalRow,
                       PageIndex = request.PageIndex,
                       PageSize = request.PageSize,
                       Items = data
                   };

                   return new ApiSuccessResult<PagedResult<PostVm>>(pagedResult);
               }
               catch (Exception ex)
               {
                   return new ApiErrorResult<PagedResult<PostVm>>($"An error occurred while getting paged posts: {ex.Message}");
               }
           }*/
    }
}