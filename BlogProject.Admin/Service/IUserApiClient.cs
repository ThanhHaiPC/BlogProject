using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;

namespace BlogProject.Admin.Service
{
    public interface IUserApiClient
    {
        Task<string> Authencate(LoginRequest request);
        Task<PagedResult<UserVm>> GetUserPaging(GetUserPagingRequest request);
        Task<bool> RegisterUser(RegisterRequest registerRequest);
    }
}
