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
        Task<ApiResult<UserVm>> GetById(Guid id);
		Task<ApiResult<UserVm>> Profile(Guid id);
		Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
