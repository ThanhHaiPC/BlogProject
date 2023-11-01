using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Comments
{
    public interface ICommentService 
    {
        Task<ApiResult<bool>> Create(CommentCreateRequest request);
        Task<ApiResult<bool>> Delete(int id);
        Task<List<Comment>> GetById(int id);
    }
}
