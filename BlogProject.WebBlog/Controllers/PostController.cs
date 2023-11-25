using BlogProject.Apilntegration.Category;
using BlogProject.Apilntegration.Comments;
using BlogProject.Apilntegration.Posts;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogProject.ViewModel.Catalog.Like;
using BlogProject.ViewModel.Catalog.Comments;

namespace BlogProject.WebBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostApiClient _postApiClient;
        private readonly ICommentsApiClient _commentApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IHttpContextAccessor _contextAccessor;
        public PostController(IPostApiClient postApiClient, ICommentsApiClient commentApiClient, ICategoryApiClient categoryApiClient, IHttpContextAccessor contextAccessor)
        {
            _postApiClient = postApiClient;
            _commentApiClient = commentApiClient;
            _categoryApiClient = categoryApiClient;
            _contextAccessor = contextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Detail (int id)
        {
			var Popular = await _postApiClient.TakeTopByQuantity(3);
			ViewData["PostPopular"] = Popular;
			var PostRecent = await _postApiClient.RecentPost(3);
			ViewData["PostRecent"] = PostRecent;
            var PostInDay = await _postApiClient.GetPostInDay();
            ViewData["PostInDay"] = PostInDay;
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			var post = await _postApiClient.Detial(id);
			var comments = await _commentApiClient.GetAllByPostId(id);

			var model = new PostDetailRequest
			{
				Post = post.ResultObj,
				Comments = comments// Đảm bảo kiểu dữ liệu của Comments là List<CommentVm>
			};
			return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ListPostOfCategory(int categoryId)
        {
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
            
			var post = await _postApiClient.GetPostOfCategory(categoryId);
           
            return View(post);
        }
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Like(int postId)
		{

			if (User.Identity.Name == null)
			{
				return BadRequest();
			}

			var addlike = new LikeVm
			{


				Username = User.Identity.Name,
				Id = postId,		
				
			};
			var result = await _postApiClient.Like(addlike);

			if (result.IsSuccessed)
			{
				return Ok();
			}

			return BadRequest();
		}

	}
}
