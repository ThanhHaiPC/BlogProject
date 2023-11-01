using BlogProject.Application.Catalog.Comments;
using BlogProject.ViewModel.Catalog.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(CommentCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryId = await _commentService.Create(request);
            if (!categoryId.IsSuccessed)
                return BadRequest(categoryId);

            return Ok(categoryId);
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
    }
}
