using BlogProject.Application.Catalog.Comments;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _commentService.Create(request, userId);

            if (result.IsSuccessed)
            {
                return Ok(result);
            }
            else
            {
                // Handle the case when the creation was not successful
                return BadRequest(result);
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            var result = await _commentService.Delete(commentId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comments = await _commentService.GetById(id);
            return Ok(comments);
        }

        [HttpGet("comments/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(int postId, [FromQuery] GetUserPagingRequest request)
        {


            var comments = await _commentService.GetCommentsByPost(postId, request);
            if (comments != null)
            {
                return Ok(comments);
            }
            else
            {
                return NotFound(); // Return a 404 Not Found if comments are not found for the specified post.
            }
        }
    }
}
