using BlogProject.Application.Catalog.Comments;
using BlogProject.Application.Catalog.Post;
using BlogProject.Application.Catalog.Replies;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Catalog.Replies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IRepliesService _repliesService;
        private readonly IPostService _postService;
        public RepliesController(IRepliesService repliesService, IPostService postService)
        {
            _repliesService = repliesService;
            _postService = postService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReplyCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var replyResult = await _repliesService.Create(request,userId);

            if (!replyResult.IsSuccessed)

                return BadRequest(replyResult);           
            return Ok(replyResult);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reply = await _repliesService.Delete(id);
            if (!reply.IsSuccessed)
                return BadRequest(reply);
            return Ok(reply);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReplyByIdAsync(int id)
        {
            var reply = await _repliesService.GetReplyByIdAsync(id);
            return Ok(reply);
        }

    }
}
