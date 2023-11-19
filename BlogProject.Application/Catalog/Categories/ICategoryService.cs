﻿using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<ApiResult<bool>> Create(CategoryRequest request);
        Task<ApiResult<bool>> Update(int id, CategoryRequest request);
        Task<ApiResult<bool>> Delete(int categoryId);

        Task<ApiResult<CategoryRequest>> GetById(int categoryId);
        Task<List<CategoryVm>> GetAll();
        Task<ApiResult<PagedResult<CategoryRequest>>> GetCategoryPaging(GetUserPagingRequest request);

    }
}
