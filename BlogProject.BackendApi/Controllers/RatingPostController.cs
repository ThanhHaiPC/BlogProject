using BlogProject.Application.Catalog.Ratingss;
using BlogProject.ViewModel.Catalog.RatingPost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingPostController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingPostController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateRatingRequest request, string userID, int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool result = await _ratingService.Create(userID, postId);

            if (result)
            {
                return Ok("Create successful");  // Return a success response if Create returns true.
            }

            return BadRequest("Create failed");  // Return a failure response if Create returns false.
        }
        /* [HttpPost("rate")]
         public async Task<IActionResult> Rate(int ratingId, int star_number)
         {
             var result = await _ratingService.Rating(ratingId, star_number);

             if (result)
             {
                 return Ok(result);
             }

             return BadRequest(result);
         }*/
        [HttpGet("get-rating")]
        public async Task<IActionResult> GetRatingForPostByUser(string userID, int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int rating = await _ratingService.GetRatingForPostByUser(userId, postId);

            if (rating >= 0)
            {
                return Ok(rating);  // Return the rating if it's found.
            }

            return NotFound("Rating not found");  // Return a not found response if the rating is not found.
        }
/*
        [HttpGet("average-rating")]
        public async Task<IActionResult> GetAverageRatingForPost(int postId)
        {
            double averageRating = await _ratingService.GetAverageRatingForPost(postId);

            if (averageRating >= 0)
            {
                return Ok(averageRating);  // Return the average rating if it's found.
            }

            return NotFound("Average rating not found");  // Return a not found response if the average rating is not found.
        }*/

        [HttpGet("get-ratings")]
        public async Task<IActionResult> GetRatingsByPost(int postId)
        {
            var ratings = await _ratingService.GetRatingsByPost(postId);

            if (ratings != null && ratings.Count > 0)
            {
                return Ok(ratings);  // Return the list of ratings if they are found.
            }

            return NotFound("Ratings not found");  // Return a not found response if there are no ratings.
        }

    }
}
