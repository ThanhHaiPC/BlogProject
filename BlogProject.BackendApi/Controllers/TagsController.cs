using BlogProject.Application.Catalog.Tags;
using BlogProject.ViewModel.Catalog.Tags;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTag(TagCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tag = await _tagService.Create(request);
            if (!tag.IsSuccessed)
                return BadRequest(tag);

            return Ok(tag);
        }
        [HttpDelete("{tagId}")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            var result = await _tagService.DeleteTag(tagId);
            return Ok(result);
        }

        [HttpPut("{tagId}")]
        public async Task<IActionResult> UpdateTag(int tagId, TagUpdateRequest request)
        {
            var result = await _tagService.UpdateTag(tagId, request);
            if (result.IsSuccessed)
            {
                return Ok("Tag updated successfully");
            }
            else
            {
                return NotFound("Tag not found or failed to update.");
            }
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTags();
            return Ok(tags);
        }

        [HttpGet("{tagId}")]
        public async Task<IActionResult> GetTagById(int tagId)
        {
            var tag = await _tagService.GetTagById(tagId);
            if (tag != null)
            {
                return Ok(tag);
            }
            else
            {
                return NotFound("Tag not found.");
            }
        }
        [HttpGet("get-posts-for-tag/{PostId}")]
        public async Task<IActionResult> GetPostsForTag(int PostId)
        {
            var comments = await _tagService.GetPostsForTag(PostId);

            if (comments != null)
            {
                return Ok(comments);
            }
            else
            {
                return NotFound(); // Return a 404 Not Found if comments are not found for the specified post.
            }
        }
        [HttpGet("get-all-paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {

            var post = await _tagService.GetTagPaging(request);
            return Ok(post);
        }
    }
}
