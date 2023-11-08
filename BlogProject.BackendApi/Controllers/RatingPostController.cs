using BlogProject.Application.Catalog.Ratingss;
using BlogProject.ViewModel.Catalog.RatingPost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create([FromBody] CreateRatingRequest request)
        {
            var result = await _ratingService.Create(request.UserId, request.PostId, request.RatingValue);
            if (result)
            {
                return Ok("Rating created successfully");
            }
            return BadRequest("Failed to create rating");
        }

        [HttpPost("rating")]
        public async Task<IActionResult> Rating(int id, int star_number)
        {

            if (id <= 0 || star_number < 1 || star_number > 5)
            {
                // You can return a BadRequest result for invalid input.
                return BadRequest("Invalid input parameters");
            }

            bool result = await _ratingService.Rating(id, star_number);

            if (result)
            {
                // Return an OK result if rating is successful.
                return Ok("Rating successful");
            }

            // Return a BadRequest result if rating is not successful.
            return BadRequest("Rating unsuccessful");
        }
    }
}
