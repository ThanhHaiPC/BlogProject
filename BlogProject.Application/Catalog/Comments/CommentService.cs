using BlogProject.Application.Catalog.Post;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;
        public CommentService(BlogDbContext context, IUserService userService, UserManager<User> userManager, IPostService postService)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _postService = postService;
        }

        public async Task<List<CommentVm>> CommentsByPost(int postId)
        {
            var comment = await _context.Comments
                .Where(x => x.PostID == postId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.User)
                .ToListAsync();
            var commentVm = new List<CommentVm>();
            foreach (var item in comment)
            {
                CommentVm vm = new CommentVm();
                vm.postId = postId;
                vm.UserName = item.User.UserName;
                vm.Avatar = item.User.Image;
                vm.Content = item.Content;
                vm.DateTime = item.Date;

                commentVm.Add(vm);
            }
            return commentVm;
        }

        public async Task<int> CountAsyncById(int id)
        {
            return await _context.Comments.Where(x => x.PostID == id).CountAsync();
        }
        public int CountById(int id)
        {
            return _context.Comments.Where(x => x.PostID == id).Count();
        }

        public async Task<ApiResult<bool>> Create(CommentCreateRequest request)
        {

            Guid userId = await _userService.GetIdByUserName(request.UserName);
            var comment = new Comment();
            comment.UserId = userId;
            comment.PostID = request.PostID ?? 0;

            comment.Date = DateTime.Now;
            comment.Content = request.Content;
            comment.Like = 0;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var post = await _context.Posts.Where(x => x.PostID == request.PostID).FirstOrDefaultAsync();
            var content = request.UserName + " vừa bình luận bài viết '" + post.Title + "' của bạn lúc " + DateTime.Now.ToString("HH:mm, dd/MM/yyyy");


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

        public async Task<PagedResult<CommentVm>> GetCommentsByPost(int postId, GetUserPagingRequest request)
        {
            var commentId = await _context.Comments
               .Include(c => c.User)
               .OrderByDescending(x => x.Date)
               .Where(p => p.PostID == postId)
               .ToListAsync();


            if (!string.IsNullOrEmpty(request.Keyword))
            {
                commentId = (List<Comment>)commentId.Where(x => x.Content.Contains(request.Keyword));
            }

            int totalRow = commentId.Count();
            var data = commentId.Skip((request.PageIndex - 1) * request.PageSize)
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

        public async Task<List<CommentVm>> GetList(int postId)
        {
            var comment = await _context.Comments.Include(x => x.User).OrderByDescending(o => o.Date).Where(x => x.PostID == postId).ToListAsync();
            List<CommentVm> result = new List<CommentVm>();
            foreach (var commentVm in comment)
            {
                CommentVm cm = new CommentVm();
                cm.Content = commentVm.Content;
                cm.DateTime = commentVm.Date;
                cm.Avatar = commentVm.User.Image;
                cm.UserName = commentVm.User.UserName;
                result.Add(cm);
            }
            return result;
        }
    }
}