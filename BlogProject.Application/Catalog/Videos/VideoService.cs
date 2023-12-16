using BlogProject.Application.Catalog.Likes;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.Data.Enum;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Catalog.Videos;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Videos
{
    public class VideoService : IVideoService
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        /*    private readonly ICommentService _commentService;*/
        private readonly ILikeService _likeService;
        public Task<ApiResult<bool>> CreateVideo(string userId, VideoVm request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> CreateVideo(string userId, VideoCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> Delete(int videoID)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> EditVideo(int videoID, VideoVm request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<PagedResult<VideoVm>>> GetPaged(GetUserPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<VideoVm>> GetPagedUser(GetUserPagingRequest request, string userId)
        {
            var video = await _context.Videos
                     .Where(p => p.UserId == Guid.Parse(userId) && p.Active == Active.yes)
                     .Include(p => p.User)

                     .Include(p => p.Categories)
                     .ToListAsync();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                video = (List<Video>)video.Where(x => x.Title.Contains(request.Keyword) || x.Description.Contains(request.Keyword));
            }
            int totalRow = video.Count();
            var postsWithUsernames = video.Select(post => new VideoVm
            {
                Id = post.VideoID,
                UserName = post.User.UserName,
                Title = post.Title,
                ImageURL = post.ImageUrl,
                Update = post.UpDate,
                ViewCount = post.View,
                
                CategoryName = post.Categories.Name,
                Description = post.Description,
               
                CommentCount = _context.Comments
                       .Where(c => c.VideoID == post.VideoID)
                       .Count(),
                LikeCount = _context.Likes
                        .Where(l=>l.VideoID == post.VideoID)
                        .Count(),
            }).ToList();

            var pagedResult = new PagedResult<VideoVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = postsWithUsernames
            };
            return pagedResult;
        }
    }
}
