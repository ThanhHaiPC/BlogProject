using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Videos;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Videos
{
    public interface IVideoService
    {
        public Task<ApiResult<PagedResult<VideoVm>>> GetPaged(GetUserPagingRequest request);
        public Task<ApiResult<bool>> CreateVideo(string userId,VideoCreateRequest request);
        public Task<ApiResult<bool>> EditVideo(int videoID,VideoVm request);
        public Task<ApiResult<bool>> Delete(int videoID);
        public Task<PagedResult<VideoVm>> GetPagedUser(GetUserPagingRequest request, string userId);
    }   
}
