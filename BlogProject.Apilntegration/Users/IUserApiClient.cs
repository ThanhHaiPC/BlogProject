using BlogProject.ViewModel.Catalog.Like;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;

namespace BlogProject.Apilntegration.Users
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetUserPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        Task<ApiResult<bool>> DeleteUser(Guid id);
        Task<ApiResult<bool>> Follow(FollowViewModel request);
        Task<ApiResult<UserVm>> GetById(Guid id);
		Task<ApiResult<UserVm>> Profile(Guid id);
		Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
		Task<ApiResult<UserVm>> GetByUserName(string username);
		Task<ApiResult<PagedResult<FollowVm>>> GetFollowersPagings(GetUserPagingRequest request);
        Task<ApiResult<bool>> CheckFollow(FollowViewModel request);
        Task<ApiResult<bool>> UserUpdate(UpdateUserRequest request, Guid id);
        Task<ApiResult<bool>> ForgotPass(string email);
        Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordViewModel request);
        Task<ApiResult<bool>> ForgotPassAdmin(string email);
        Task<ApiResult<bool>> ChangePass(ChangePassword request, Guid id);
    }
}
