using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Roles;

namespace BlogProject.Apilntegration.Roles
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}