using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Replies;
using BlogProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Replies
{
    public class RepliesService : IRepliesService
    {
        private readonly BlogDbContext _context;
        private readonly IUserService _userService;

        public RepliesService(BlogDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<ApiResult<bool>> Create(ReplyCreateRequest request)
        {
            // Tìm userId dựa trên userName
            Guid userId = await _userService.GetIdByUserName(request.UserName);

            // Tạo một đối tượng Comment từ dữ liệu trong request
            var comment = new Comment
            {
                UserId = userId,
                Date = DateTime.Now,
                Content = request.Content,
            };

            // Set the CommentID to the parent comment's ID if it exists
            comment.CommentID = request.CommentID ?? 0; // Use the null-coalescing operator to handle nullable int

            // Lưu đối tượng Comment vào cơ sở dữ liệu 
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Lấy thông tin bài viết liên quan đến comment
            var cm = await _context.Comments.Where(x => x.CommentID == request.CommentID).FirstOrDefaultAsync();

            // Tạo nội dung thông báo
            var content = $"{request.UserName} vừa trả lời bình luận của bạn lúc {DateTime.Now.ToString("HH:mm, dd/MM/yyyy")}";
            return new ApiSuccessResult<bool>();
        }

        public async  Task<ApiResult<bool>> Delete(int id)
        {
            var reply = await _context.Replies.FirstOrDefaultAsync(x => x.ReplyID == id);
            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }
        public async  Task<List<Reply>> GetReplyByIdAsync(int id)
        {
            return await _context.Replies.Where(x => x.CommentID == id).ToListAsync();
        }
    }
}
