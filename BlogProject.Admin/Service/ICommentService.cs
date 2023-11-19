using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;

namespace BlogProject.Admin.Service
{
    public interface ICommentService
    {
        Task<PagedResult<CommentVm>> GetByPostId(int postId, GetUserPagingRequest request);
    }
}
