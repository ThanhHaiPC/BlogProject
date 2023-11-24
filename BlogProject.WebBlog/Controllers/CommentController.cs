using BlogProject.Apilntegration.Comments;

using BlogProject.ViewModel.Catalog.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace BlogProject.WebBlog.Controllers
{
	public class CommentController : Controller
	{
		private readonly ICommentsApiClient _commentApiClient;
		public CommentController(ICommentsApiClient commentApiClient)
		{
			_commentApiClient = commentApiClient;
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddComment(int postId, string content)
		{
			if (User.Identity.Name == null)
			{
				return BadRequest();
			}
			var newComment = new CommentCreateRequest
			{
			
				
				UserName = User.Identity.Name,
				PostID = postId,
				Content = content,
				Date = DateTime.Now
			};
			var result = await _commentApiClient.Create(newComment);
		
			if (result.IsSuccessed)
			{
				return Ok();
			}
			return PartialView();
		}
        [HttpGet]
        public async Task<IActionResult> GetComments(int postId)
        {
			var comments = await _commentApiClient.GetById(postId);
			
			return PartialView("/Views/Post/_CommentList.cshtml", comments);
        }

    }
}
