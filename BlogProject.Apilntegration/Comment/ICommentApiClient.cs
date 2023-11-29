using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Apilntegration.Comment
{
    public interface ICommentApiClient
    {
        Task<PagedResult<CommentVm>> GetByPostId(int postId, GetUserPagingRequest request);
        Task<List<CommentVm>> GetAllByPostId(int postId);
        Task<List<CommentVm>> GetById(int id);
        Task<ApiResult<bool>> Create(CommentCreateRequest request);
    }
}
