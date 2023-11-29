using BlogProject.Apilntegration.Category;
using BlogProject.Apilntegration.Comment;
using BlogProject.Apilntegration.Posts;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogProject.ViewModel.Catalog.Like;
using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.Apilntegration.Tags;

namespace BlogProject.WebBlog.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostApiClient _postApiClient;
		private readonly ICommentApiClient _commentApiClient;
		private readonly ICategoryApiClient _categoryApiClient;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly ITagApiClient _tagApiClient;
		public PostController(IPostApiClient postApiClient, ICommentApiClient commentApiClient, ICategoryApiClient categoryApiClient, IHttpContextAccessor contextAccessor,ITagApiClient tagApiClient)
		{
			_postApiClient = postApiClient;
			_commentApiClient = commentApiClient;
			_categoryApiClient = categoryApiClient;
			_contextAccessor = contextAccessor;
			_tagApiClient = tagApiClient;
		}
		[HttpGet]
		public async Task<IActionResult> Detail(int id)
		{
			var likeVm = new LikeVm();

			likeVm.Id = id;
			likeVm.Username = User.Identity.Name;

			var Popular = await _postApiClient.TakeTopByQuantity(10);
			ViewData["PostPopular"] = Popular;
			var PostRecent = await _postApiClient.RecentPost(10);
			ViewData["PostRecent"] = PostRecent;
			var PostInDay = await _postApiClient.GetPostInDay();
			ViewData["PostInDay"] = PostInDay;
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			var Tag = await _tagApiClient.GetAll();
			ViewData["Tag"] = Tag;
			var post = await _postApiClient.Detial(id);
			var comments = await _commentApiClient.GetAllByPostId(id);

			var checkLike = await _postApiClient.Check(likeVm);
			if (checkLike.IsSuccessed == true)
			{
				ViewBag.CheckLike = true;
			}
			else { ViewBag.CheckLike = false; }

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