﻿using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Post
{
    public interface IPostService
    {
        Task<ApiResult<List<PostVm>>> GetAll();
        Task<PagedResult<PostVm>> GetPaged(GetUserPagingRequest request);
        Task<ApiResult<bool>> Create(PostRequest request, string userId);
        Task<ApiResult<bool>> Update(PostUpdateRequest request, int Id);
        Task<ApiResult<bool>> Delete(int Id);
        Task<PagedResult<PostVm>> GetByUserId(string userId, GetUserPagingRequest request);
        Task<ApiResult<List<PostVm>>> Search(string searchTerm);
        Task<ApiResult<PostVm>> GetById(int Id);
        Task<List<PostVm>> TakeTopByQuantity(int quantity);
        /* Task<ApiResult<PagedResult<PostVm>>> GetPostFollowPaging(GetUserPagingRequest request);*/
    }
}
