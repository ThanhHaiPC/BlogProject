using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Roles;

namespace BlogProject.Admin.Service
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}
