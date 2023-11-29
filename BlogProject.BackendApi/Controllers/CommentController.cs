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

        public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _commentService.Create(request);

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
        [HttpGet("comment/{postId}")]
        public async Task<IActionResult> CommentOfPost(int postId)
        {
            var comment = await _commentService.CommentsByPost(postId);
            if (comment != null)
            {
                return Ok(comment);
            }
            else
            {
                return NotFound(); // Return a 404 Not Found if comments are not found for the specified post.
            }
        }
        [HttpGet("listComment/{postId}")]
        public async Task<IActionResult> GetList(int postId)
        {
            var comment = await _commentService.GetList(postId);
            if (comment != null)
            {
                return Ok(comment);
            }
            else
            {
                return NotFound(); // Return a 404 Not Found if comments are not found for the specified post.
            }
        }
    }
}