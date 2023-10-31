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
        public Task<ApiResult<bool>> Create()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Delete(int Id)
        {
            throw new NotImplementedException();
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

        public Task<ApiResult<bool>> Update(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
