using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Catalog.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Tags
{
    public interface ITagService
    {
        Task<int> CreateTag(TagCreateRequest request);
        Task<bool> UpdateTag(int tagId, TagUpdateRequest request);
        Task<bool> DeleteTag(int tagId);
        Task<List<TagVm>> GetAllTags();
        Task<TagVm> GetTagById(int tagId);
        Task<List<Tag>> GetPostsForTag(int tagId);
        /* Task<List<TagVm>> GetTagsByPopularity(int topN);*/
    }
}
