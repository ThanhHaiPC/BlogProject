using BlogProject.Application.Catalog.Comments;
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
        public RepliesController(IRepliesService repliesService)
        {
            _repliesService = repliesService;
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
        [HttpDelete("{replyId}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repliesService.Delete(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReplyByIdAsync(int id)
        {
            var reply = await _repliesService.GetReplyByIdAsync(id);
            return Ok(reply);
        }

    }
}
