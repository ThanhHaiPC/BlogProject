using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
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
        Task<PagedResult<CommentVm>> GetCommentsByPost(int postId, GetUserPagingRequest request);
        Task<List<CommentVm>> CommentsByPost(int postId);
        Task<List<CommentVm>> GetList(int postId);
        Task<int> CountAsyncById(int id);
        int CountById(int id);
    }
}
