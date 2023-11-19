using BlogProject.Application.Catalog.Post;
using BlogProject.Application.Catalog.Replies;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogProject.Application.Catalog.Comments
{
    public class CommentService : ICommentService
    {
        private readonly BlogDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
   

        public CommentService(BlogDbContext context, IUserService userService, UserManager<User> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
      
           
        }

        public async Task<int> CountAsyncById(int id)
        {
            return await _context.Comments.Where(x => x.PostID == id).CountAsync();
        }
        public int CountById(int id)
        {
            return _context.Comments.Where(x => x.PostID == id).Count();
        }

        public async Task<ApiResult<bool>> Create(CommentCreateRequest request, string userId)
        {

            
            var user = await _userManager.FindByIdAsync(userId);


            // Tạo một đối tượng Comment từ dữ liệu trong request
            var comment = new Comment
            {
                UserId = user.Id,
                PostID = request.PostID ?? 0,
                Date = DateTime.Now,
                Content = request.Content,
                Like = 0
            };

            // Lưu đối tượng Comment vào cơ sở dữ liệu 
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Lấy thông tin bài viết liên quan đến comment
            var post = await _context.Posts.Where(x => x.PostID == request.PostID).FirstOrDefaultAsync();

            // Tạo nội dung thông báo
            var content = $"{request.UserName} vừa bình luận bài viết '{post.Title}' của bạn lúc {DateTime.Now.ToString("HH:mm, dd/MM/yyyy")}";
            return new ApiSuccessResult<bool>();

        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentID == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async Task<List<Comment>> GetById(int id)
        {
            return await _context.Comments.Where(x => x.CommentID == id).ToListAsync();
        }

        public async Task<PagedResult<CommentVm>> GetCommentsByPost(int Id, GetUserPagingRequest request)
        {
            var commentId = await _context.Comments
                .Include(c => c.User)
                .Where(p => p.PostID == Id)
                .ToListAsync();


            if (!string.IsNullOrEmpty(request.Keyword))
            {
                commentId = (List<Comment>)commentId.Where(x => x.Content.Contains(request.Keyword));
            }

            int totalRow =  commentId.Count();
            var data =  commentId.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .Select(x => new CommentVm()
                 {
                    
                    UserName = x.User.UserName,
                    Content = x.Content,
                    DateTime = x.Date, 
                }).ToList();
            //4. Select and projection
            var pagedResult = new PagedResult<CommentVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;


        }
    }
}
