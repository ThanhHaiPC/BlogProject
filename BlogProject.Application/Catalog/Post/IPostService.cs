using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Post;
using BlogProject.ViewModel.Common;
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
        Task<ApiResult<bool>> Create();
        Task<ApiResult<bool>> Update(int Id);
        Task<ApiResult<bool>> Delete(int Id);
    }
}
