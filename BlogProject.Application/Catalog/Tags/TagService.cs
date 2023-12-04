using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.Data.Enum;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Catalog.Tags;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Tags
{
    public class TagService : ITagService
    {
        private readonly BlogDbContext _dbContext;
      
        public TagService(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<bool>> Create(TagCreateRequest request)
        {
            var PostExists = await _dbContext.Posts.AnyAsync(c => c.PostID == request.PostID);
            if (!PostExists)
            {
                return new ApiErrorResult<bool>("PostID không tồn tại");
            }
            var add = new Tag
            {
                TagName = request.TagName,
                UploadDate = DateTime.Now,
             
            };
            _dbContext.Tags.Add(add);
            await _dbContext.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async  Task<ApiResult<bool>> DeleteTag(int tagId)
        {
            var tag = await _dbContext.Tags.FindAsync(tagId);
            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async  Task<List<TagVm>> GetAllTags()
        {

            var tags = await _dbContext.Tags.ToListAsync(); // Assuming _dbContext is your database context.

            var tagVms = tags.Select(tag => new TagVm
            {
                TagId = tag.TagID,
                TagName = tag.TagName,
                // You can map other properties as needed.
            }).ToList();

            return tagVms;
        }

        public async  Task<List<PostVm>> GetPostsForTag(int PostId)
        {
            var posts = await _dbContext.Posts
            .Where(c => c.TagId == PostId)
            .Include(c=>c.User)
            .Include(c=>c.Categories)
            
            .ToListAsync();
            List<PostVm> postVms = new List<PostVm>();
            foreach (var post in posts)
            {
                PostVm postVm = new PostVm();
                post.Title = postVm.Title;
                post.Desprition = postVm.Desprition;
                post.Image = postVm.Image;
                post.User.UserName = postVm.UserName;
                post.Categories.Name = postVm.CategoryName;
                post.UploadDate = postVm.UploadDate;
                postVms.Add(postVm);

            }
            return postVms;
        }


        public async Task<ApiResult<TagVm>> GetTagById(int tagId)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.TagID == tagId);

            if (tag == null)
            {
                // Handle the case where the tag is not found based on the provided tagId
                return null; // Or throw an exception or return a default TagVm object
            }

            var tagVm = new TagVm
            {
                TagId = tag.TagID,
                TagName = tag.TagName,
             
                UploadDate = tag.UploadDate,
          
                // Include other properties here as needed based on your TagVm structure
            };

            return new ApiSuccessResult<TagVm>(tagVm);
        }

        public async  Task<ApiResult<PagedResult<TagVm>>> GetTagPaging(GetUserPagingRequest request)
		{
            var query = from t in _dbContext.Tags
                        select new { t };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.t.TagName.Contains(request.Keyword)); // Thay vì tìm kiếm trong tiêu đề và mô tả, ở đây ta tìm kiếm trong tên tag
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TagVm()
                {
                    TagId = x.t.TagID, // Sử dụng Id của tag
                    TagName = x.t.TagName,
                    // Các thông tin khác về tag mà bạn muốn hiển thị
                    // Ví dụ: Description, CountPost (số lượng bài viết liên quan đến tag này)
                }).ToListAsync();

            var pagedResult = new PagedResult<TagVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return new  ApiSuccessResult<PagedResult<TagVm>>(pagedResult);
        }

        public async Task<bool> UpdateTag(int tagId, TagUpdateRequest request)
        {
            var tag = await _dbContext.Tags.FindAsync(tagId);

            tag.TagName = request.TagName;

            _dbContext.Tags.Update(tag);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
