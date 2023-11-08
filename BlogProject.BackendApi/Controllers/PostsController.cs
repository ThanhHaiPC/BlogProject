using BlogProject.Application.Catalog.Post;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostsController(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;

            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {

            var post = await _postService.GetAll();
            return Ok(post);
        }
        [HttpPost("/Post/Create")]
        public async Task<IActionResult> Create([FromForm] PostRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var post = await _postService.Create(request, userId);
            if (!post.IsSuccessed)
                return BadRequest(post);

            return Ok(post);
        }
        [HttpDelete("/Post/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = await _postService.Delete(Id);
            if (!post.IsSuccessed)
                return BadRequest(post);
            return Ok(post);
        }
        [HttpPut("/Post/Update/{Id}")]
        public async Task<IActionResult> Update([FromForm] PostUpdateRequest request, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = await _postService.Update(request, Id);
            if (!post.IsSuccessed)
                return BadRequest(post);
            return Ok(post);
        }
        [HttpGet("get-all-paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {

            var post = await _postService.GetPaged(request);
            return Ok(post);
        }
    }
}
