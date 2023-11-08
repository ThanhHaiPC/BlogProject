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
      
            Task<bool> Create(string userID, int postId);
        /* Task<bool> Rating(int ratingId, int star_number);*/
        Task<int> GetRatingForPostByUser(string userID, int postId);
        /*Task<double> GetAverageRatingForPost(int postId);*/
        Task<List<Rating>> GetRatingsByPost(int postId);
    }
}
