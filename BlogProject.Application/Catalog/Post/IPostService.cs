using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Post
{
    public interface IPostService
    {
        Task<ApiResult<List<PostVm>>> GetAll();
        Task<PagedResult<PostVm>> GetPaged(GetUserPagingRequest request);
        Task<ApiResult<bool>> Create(PostRequest request, string userId);
        Task<ApiResult<bool>> Update(PostUpdateRequest request, int Id);
        Task<ApiResult<bool>> Delete(int Id);
    }
}
