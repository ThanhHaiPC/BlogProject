using BlogProject.ViewModel.System.Users;

namespace BaseProject.AdminApp.Service
{
	public interface IUserApiClient
	{
		Task<string> Authenticate(LoginRequest request);
	}
}
