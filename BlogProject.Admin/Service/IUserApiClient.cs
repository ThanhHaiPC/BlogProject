using BlogProject.ViewModel.System.Users;

namespace BlogProject.Admin.Service
{
    public interface IUserApiClient
    {
        Task<string> Authencate(LoginRequest request);
    }
}
