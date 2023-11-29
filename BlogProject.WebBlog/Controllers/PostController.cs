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
using Microsoft.AspNetCore.Identity;
using BlogProject.Data.Entities;
using BlogProject.Apilntegration.Users;

namespace BlogProject.WebBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostApiClient _postApiClient;
        private readonly ICommentsApiClient _commentApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IHttpContextAccessor _contextAccessor;
		private readonly IUserApiClient _userApiClient;
        public PostController(IPostApiClient postApiClient, IUserApiClient userApiClient, ICommentsApiClient commentApiClient, ICategoryApiClient categoryApiClient, IHttpContextAccessor contextAccessor)
        {
            _postApiClient = postApiClient;
            _commentApiClient = commentApiClient;
            _categoryApiClient = categoryApiClient;
            _contextAccessor = contextAccessor;
			_userApiClient = userApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Detail (int id)
        {
			var likeVm = new LikeVm();

			likeVm.Id = id;
			likeVm.Username = User.Identity.Name;

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
				var post = await _postApiClient.GetById(postId);
				return Ok(post);
			}

			return BadRequest();
		}
		[HttpGet]
		public async Task<IActionResult> Search(string keyword, int pageIndex=1, int pageSize=5) 
		{
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			var request = new GetUserPagingRequest()
			{
				Keyword = keyword,
				PageIndex = pageIndex,
				PageSize = pageSize
			};
			
			var data = await _postApiClient.GetAllPaging(request);
			return View(data);
		}
		[HttpGet]
		public async Task<IActionResult> UserPost(string userId)
		{
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			var user = await _userApiClient.GetById(Guid.Parse(userId));
			ViewData["user"] = user.ResultObj;
			var post = await _postApiClient.GetByUserId(userId);
			return View(post);
		}
	}
}
