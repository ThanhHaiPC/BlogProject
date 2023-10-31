using BlogProject.Application.Catalog.Categories;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Post;
using BlogProject.ViewModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Post
{
    public class PostService : IPostService
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<User> _userManager;
        
        public PostService(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(PostRequest request)
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
            if (request.Image == null)
            {
                return new ApiErrorResult<bool>("Chưa có hình ảnh");
            }

            List<Posts> posts = await _context.Posts.Where(x=>x.Title == request.Title).ToListAsync();
            if (posts.Count != 0)
            {
                return new ApiErrorResult<bool>("Bài viết đã tồn tại");
            }
            var add = new Posts
            { 
                Title = request.Title,
                Content = request.Content,
                Desprition = request.Desprition,
                Image = request.Image,
            };
            _context.Posts.Add(add);
            _context.SaveChangesAsync();
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
                Image = post.Image,
                
                
            }).ToList();
            return new ApiSuccessResult<List<PostVm>>(postsWithUsernames);
        }

        public Task<ApiResult<bool>> Update(PostRequest request,int Id)
        {
            throw new NotImplementedException();
        }
    }
}
