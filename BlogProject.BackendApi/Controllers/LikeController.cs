using BlogProject.Application.Catalog.Comments;
using BlogProject.Application.Catalog.Likes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        // GET api/like/{id}
        [HttpGet("{id}")]
        public ActionResult<int> GetLikeCount(int id)
        {
            int likeCount = _likeService.CountById(id);
            return Ok(likeCount);
        }

        // GET api/like/async/{id}
        [HttpGet("async/{id}")]
        public async Task<ActionResult<int>> GetLikeCountAsync(int id)
        {
            int likeCount = await _likeService.CountAsyncById(id);
            return Ok(likeCount);
        }
    }
}
