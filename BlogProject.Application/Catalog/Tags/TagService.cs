using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.Data.Enum;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Catalog.Tags;
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
  
        public async  Task<int> CreateTag(TagCreateRequest request)
        {
           
            // Here, you would typically add logic to create a new tag in your database.
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.PostID == request.PostID);
            // For example, if you're using Entity Framework, you can create a new Tag entity:
            var newTag = new Tag
                {
             
                TagName = request.TagName,
                    // Set other properties if necessary
                };

                 _dbContext.Tags.Add(newTag);
                await _dbContext.SaveChangesAsync();

                // After saving the new tag to the database, you can return its ID.
                return newTag.TagID;          
        }

        public async  Task<bool> DeleteTag(int tagId)
        {
            var tag = await _dbContext.Tags.FindAsync(tagId);
            if (tag == null)
            {
                // Tag not found, return false or handle the situation accordingly.
                return false;
            }

            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
            return true;
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

        public async  Task<List<Tag>> GetPostsForTag(int tagId)
        {
            var comments = await _dbContext.Tags
            .Where(c => c.PostID == tagId)
            .ToListAsync();

            return comments;
        }

       
        public async  Task<TagVm> GetTagById(int tagId)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.TagID == tagId);
            var tagVm = new TagVm
            {
                TagId = tag.TagID,
                TagName = tag.TagName,
            };

            return tagVm;
        }

       
        public async  Task<bool> UpdateTag(int tagId, TagUpdateRequest request)
        {
            var tag = await _dbContext.Tags.FindAsync(tagId);

            tag.TagName = request.TagName;

            _dbContext.Tags.Update(tag);
            await _dbContext.SaveChangesAsync();
            return true;
        }

       
    }
}
