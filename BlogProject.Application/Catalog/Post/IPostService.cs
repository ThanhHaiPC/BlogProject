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

namespace BlogProject.Application.Catalog.Post
{
	public interface IPostService
	{
		Task<ApiResult<List<Posts>>> GetAll();
		Task<PagedResult<PostVm>> GetPaged(GetUserPagingRequest request);
		Task<ApiResult<bool>> Create(PostRequest request, string userId);
		Task<ApiResult<bool>> Update(PostUpdateRequest request, int Id);
		Task<ApiResult<bool>> Delete(int Id);
        Task<List<PostVm>> GetByUserId(string userId);
        Task<ApiResult<List<PostVm>>> Search(string searchTerm);
		Task<ApiResult<PostVm>> GetById(int Id);
		Task<ApiResult<PostVm>> DetalUser(int Id);
		Task<List<PostVm>> GetPostInDay();
		Task<List<PostVm>> GetPostOfCategory(int categoryId);
		Task<List<PostVm>> PostRecent(int quantity);
		Task<List<PostVm>> TakeTopByQuantity(int quantity);
		Task<PagedResult<PostVm>> GetByUserId(string userId, GetUserPagingRequest request);
		Task<ApiResult<bool>> Like(LikeVm request, string userId);
		Task<Like> CheckLike(string UserName, int Id);
        Task<ApiResult<bool>> Enable(PostEnable request);
        /* Task<ApiResult<PagedResult<PostVm>>> GetPostFollowPaging(GetUserPagingRequest request);*/
    }
}