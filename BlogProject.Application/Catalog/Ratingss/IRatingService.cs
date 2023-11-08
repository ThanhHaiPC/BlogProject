using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Ratingss
{
    public interface IRatingService
    {
      
            Task<bool> Create(Guid userID, int postId, int ratingValue);
            Task<bool> Rating(int ratingId, int star_number);
        /*    Task<ApiResult<PagedResult<Posts>>> GetPostsPaging(GetUserPagingRequest request);
            Task<ApiResult<Posts>> GetPostById(int postId);
            Task<ApiResult<PagedResult<Rating>>> GetRatingsByUserName(GetUserPagingRequest request);*/
        
    }
}
