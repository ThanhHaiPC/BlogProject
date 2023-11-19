﻿using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Apilntegration.Posts
{
    public interface IPostApiClient
    {
        Task<PagedResult<PostVm>> GetPagings(GetUserPagingRequest request);
        Task<ApiResult<bool>> UpdatePost(PostUpdateRequest request, int id);
        Task<ApiResult<bool>> CreatePost(PostRequest request);
        Task<ApiResult<bool>> DeletePost(int id);
       
        Task<ApiResult<PostVm>> GetById(int id);
    }
}
