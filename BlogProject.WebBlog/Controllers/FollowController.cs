using BlogProject.Apilntegration.Posts;
using BlogProject.Apilntegration.Users;
using BlogProject.Apilntegration;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Apilntegration.Category;

namespace BlogProject.WebBlog.Controllers
{
	public class FollowController : Controller
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IPostApiClient _postApiClient;
		private readonly ICategoryApiClient _categoryApiClient;

		public FollowController(IUserApiClient userApiClient, IPostApiClient postApiClient, ICategoryApiClient categoryApiClient)
		{
			_userApiClient = userApiClient;
			_postApiClient = postApiClient;
			_categoryApiClient = categoryApiClient;
		}


		[HttpGet]
		public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
		{

			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			var user = User.Identity.Name;
			
            var request = new GetUserPagingRequest()
			{
				Keyword = keyword,
				PageIndex = pageIndex,
				PageSize = pageSize,
				UserName = user
			};
			var data = await _userApiClient.GetFollowersPagings(request);
			ViewBag.Keyword = keyword;
			ViewBag.Name = user;
		
			if (TempData["result"] != null)
			{
				ViewBag.SuccessMsg = TempData["result"];
			}
			return View(data.ResultObj);
		}
		[HttpPost]
		public async Task<IActionResult> AddFollow(FollowViewModel following)
		{
			
			if (following == null)
			{
				return BadRequest();
			}
            
            var result = await _userApiClient.Follow(following);

			if (result.IsSuccessed)
			{
				return Ok();
			}
			return BadRequest();
		}

	}
}
