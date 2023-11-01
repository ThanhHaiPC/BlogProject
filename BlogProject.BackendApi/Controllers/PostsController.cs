using BlogProject.Application.Catalog.Post;
using BlogProject.ViewModel.Catalog.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            var post = await _postService.GetAll();
            return Ok(post);
        }
        [HttpPost("/Post/create")]
        public async Task<IActionResult> Create([FromForm]PostRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = await _postService.Create(request);
            if (!post.IsSuccessed)
                return BadRequest(post);

            return Ok(post);
        }
    }
}
