using BlogProject.Application.Catalog.Tags;
using BlogProject.ViewModel.Catalog.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            int newTagId = await _tagService.CreateTag(request);
            if (newTagId > 0)
            {
                return Ok(newTagId);
            }
            else
            {
                return BadRequest("Failed to create the tag.");
            }
        }
        [HttpDelete("{tagId}")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            var result = await _tagService.DeleteTag(tagId);
            if (result)
            {
                return Ok("Tag deleted successfully");
            }
            else
            {
                return NotFound("Tag not found or failed to delete.");
            }
        }

        [HttpPut("{tagId}")]
        public async Task<IActionResult> UpdateTag(int tagId, TagUpdateRequest request)
        {
            var result = await _tagService.UpdateTag(tagId, request);
            if (result)
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
        [HttpGet("get-posts-for-tag/{tagId}")]
        public async Task<IActionResult> GetPostsForTag(int tagId)
        {
            var comments = await _tagService.GetPostsForTag(tagId);

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
