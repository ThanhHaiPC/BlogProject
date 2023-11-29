using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(UserUpdateRequest request, Guid id);

        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<UserVm>> GetById(Guid id);
        Task<Guid> GetIdByUserName(string username);

        Task<ApiResult<PagedResult<UserVm>>> GetUserPaging(GetUserPagingRequest request);
        Task<string> GetUserNameByIdAsync(Guid Id);
        string GetUserNameById(Guid Id);
        Task<ApiResult<bool>> RoleAssign(RoleAssignRequest request, Guid id);
		Task<ApiResult<UserVm>> Profile(string? id);
		Task<ApiResult<UserVm>> GetByUserName(string username);
	}
}
