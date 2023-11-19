using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;

namespace BlogProject.Admin.Service
{
    public interface IPostApiClient
    {
        Task<PagedResult<PostVm>> GetPagings(GetUserPagingRequest request);
        Task<ApiResult<bool>> UpdatePost(PostUpdateRequest request, int id);
        Task<ApiResult<bool>> CreatePost(PostRequest request);
        Task<ApiResult<bool>> DeletePost(int id);
 
        Task<ApiResult<PostVm>> GetById(int id);
    }
}
