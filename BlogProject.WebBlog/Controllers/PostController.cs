using BlogProject.Apilntegration.Comments;
using BlogProject.Apilntegration.Posts;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostApiClient _postApiClient;
        private readonly ICommentsApiClient _commentApiClient;
        public PostController(IPostApiClient postApiClient, ICommentsApiClient commentApiClient)
        {
            _postApiClient = postApiClient;
            _commentApiClient = commentApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Detail (int id)
        {
			var post = await _postApiClient.Detial(id);
			var comments = await _commentApiClient.GetAllByPostId(id);

			var model = new PostDetailRequest
			{
				Post = post.ResultObj,
				Comments = comments// Đảm bảo kiểu dữ liệu của Comments là List<CommentVm>
			};
			return View(model);
        }
     

	}
}
