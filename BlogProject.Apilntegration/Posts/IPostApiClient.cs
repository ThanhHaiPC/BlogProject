using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Like;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Apilntegration.Posts
{
    public interface IPostApiClient
    {
		Task<PagedResult<PostVm>> GetPagings(GetUserPagingRequest request);
        Task<PagedResult<PostVm>> GetPagingUser(GetUserPagingRequest request);
        Task<PagedResult<PostVm>> GetAllPaging(GetUserPagingRequest request);
		Task<ApiResult<bool>> UpdatePost(PostUpdateRequest request, int id);
		Task<ApiResult<bool>> CreatePost(PostRequest request);
		Task<ApiResult<bool>> DeletePost(int id);
		Task<List<PostVm>> TakeTopByQuantity(int quantity);
		Task<List<BlogProject.Data.Entities.Posts>> PopularPost();
		Task<List<PostVm>> RecentPost(int quatity);
		Task<List<PostVm>> GetAll();
		Task<ApiResult<PostVm>> GetById(int id);
		Task<ApiResult<PostVm>> Detial(int id);
		Task<List<PostVm>> GetPostInDay();
		Task<ApiResult<bool>> Like(LikeVm request);
		Task<ApiResult<bool>> Check(LikeVm request);
		Task<List<PostVm>> GetPostOfCategory(int categoryId);
        Task<ApiResult<bool>> StatusChange(PostEnable postEnable);
        Task<ApiResult<bool>> CheckEnable(int postId);
        Task<List<PostVm>> GetByUserId(string userId);
		Task<List<BlogProject.Data.Entities.Posts>> History(string userName);
        Task<List<PostVm>> PostTrending(int quantity);
    }
}
