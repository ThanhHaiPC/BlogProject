using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Ratingss
{
    public class RatingService : IRatingService
    {

        private readonly BlogDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public RatingService(BlogDbContext context,IUserService userService, UserManager<User> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }

        public async  Task<bool> Create(string userID, int postId)
        {
            var user = await _userManager.FindByIdAsync(userID);

            var CreateRating = new Rating()
            {
                PostID = postId,
                UserId = user.Id,
                Date = DateTime.UtcNow,
                
            };
            _context.Ratings.Add(CreateRating);
            await _context.SaveChangesAsync();

            return true;
        }

      /*  public async  Task<double> GetAverageRatingForPost(int postId)
        {
            var averageRating = await _context.Ratings
        .Where(r => r.PostID == postId)
        .Select(r => r.RatingValue)
        .DefaultIfEmpty(0)
        .AverageAsync();

            return averageRating;
        }*/

        public async Task<int> GetRatingForPostByUser(string userID, int postId)
        {

            var user = await _userManager.FindByIdAsync(userID);

            var rating = await _context.Ratings
            .Where(x => x.UserId == user.Id && x.PostID == postId)
            .Select(x => x.RatingValue)
            .FirstOrDefaultAsync();

            return rating;
        }

        public async Task<List<Rating>> GetRatingsByPost(int postId)
        {
            var ratings = await _context.Ratings
             .Where(x => x.PostID == postId)
             .ToListAsync();

            return ratings;
        }





        /*  public async  Task<bool> Rating(int ratingId, int star_number)
          {
              var star = await _context.Ratings.Where(x => x.RatingID == ratingId).FirstOrDefaultAsync();
              if (star == null)
              {
                  return false;
              }


              star.Date = DateTime.UtcNow;
              star.RatingValue = star_number;


              _context.SaveChanges();

              return true;
          }*/
    }
}
