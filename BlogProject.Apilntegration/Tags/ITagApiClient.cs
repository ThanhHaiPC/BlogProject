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

namespace BlogProject.Apilntegration.Tags
{
    public interface ITagApiClient
    {
        Task<ApiResult<PagedResult<TagVm>>> GetUsersPagings(GetUserPagingRequest request);
        Task<List<TagVm>> GetAll();
        Task<ApiResult<bool>> UpdateTag(int tagId, TagUpdateRequest request);
        Task<ApiResult<bool>> DeleteTag(int tagId);

        Task<ApiResult<TagVm>> GetById(int tagId);
        Task<ApiResult<bool>> CreateTagByPost(TagCreateRequest request);
        Task<List<PostVm>> ListPostsForTag(int PostId);
    }
}
