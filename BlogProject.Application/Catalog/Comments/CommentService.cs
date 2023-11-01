using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Comments
{
    public class CommentService : ICommentService
    {
        private readonly BlogDbContext _context;
        private readonly IUserService _userService;

        public CommentService(BlogDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ApiResult<bool>> Create(CommentCreateRequest request)
        {

            // Tìm userId dựa trên userName
            Guid userId = await _userService.GetIdByUserName(request.UserName);

            // Tạo một đối tượng Comment từ dữ liệu trong request
            var comment = new Comment
            {
                UserId = userId,
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
            return await _context.Comments.Where(x => x.PostID == id).ToListAsync();
        }
    }
}
