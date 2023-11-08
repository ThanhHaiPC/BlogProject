using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
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
        private readonly RatingService _ratingService;


        public RatingService(BlogDbContext context, RatingService ratingService)
        {
            _context = context;
            _ratingService = ratingService;
        }

        public async  Task<bool> Create(Guid userID, int postId, int ratingValue)
        {
            // Create a new Rating entity
            var rating = new Rating
            {
                UserId = userID,
                PostID = postId,
                RatingValue = ratingValue,
                Date = DateTime.Now
            };

            // Add the new rating to the database
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return true; // Return true if the rating is created successfully
        }
        public async  Task<bool> Rating(int ratingId, int star_number)
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
        }
    }
}
