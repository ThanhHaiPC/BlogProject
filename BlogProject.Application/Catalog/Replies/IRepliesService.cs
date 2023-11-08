using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Catalog.Replies;
using BlogProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Replies
{
    public interface IRepliesService
    {      
        Task<ApiResult<bool>> Create(ReplyCreateRequest request, string userId);
        Task<ApiResult<bool>> Delete(int id);
        Task<List<Reply>> GetReplyByIdAsync(int id);

      /*  Task<ApiResult<bool>> GetRepliesAsync(int CommentId);*/
    }
}
