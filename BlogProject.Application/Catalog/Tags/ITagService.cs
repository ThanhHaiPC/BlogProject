using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Catalog.Tags;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Tags
{
    public interface ITagService
    {
        Task<ApiResult<bool>> Create(TagCreateRequest request);
        Task<bool> UpdateTag(int tagId, TagUpdateRequest request);
        Task<ApiResult<bool>> DeleteTag(int tagId);
        Task<List<TagVm>> GetAllTags();
        Task<ApiResult<TagVm>> GetTagById(int tagId);
        Task<List<PostVm>> GetPostsForTag(int PostId);
        Task<ApiResult<PagedResult<TagVm>>> GetTagPaging(GetUserPagingRequest request);
        /* Task<List<TagVm>> GetTagsByPopularity(int topN);*/
    }
}
