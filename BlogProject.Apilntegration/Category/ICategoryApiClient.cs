using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Apilntegration.Category
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll();
        Task<ApiResult<PagedResult<CategoryRequest>>> GetUsersPagings(GetUserPagingRequest request);
        Task<ApiResult<bool>> UpdateCategory(int idCategory, CategoryRequest request);
        Task<ApiResult<bool>> DeleteCategory(int idCategory);

        Task<ApiResult<CategoryRequest>> GetById(int id);
        Task<ApiResult<bool>> RegisterCategory(CategoryRequest request);
    }
}
