using BlogProject.Apilntegration.Posts;
using BlogProject.WebBlog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using BlogProject.Apilntegration.Category;
using BlogProject.Data.Entities;
using BlogProject.Apilntegration.Tags;
using BlogProject.ViewModel.System.Users;

namespace BlogProject.WebBlog.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IPostApiClient _postApiClient;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICategoryApiClient _categoryApiClient;
		private readonly ITagApiClient _tagApiClient;

		public HomeController(ILogger<HomeController> logger, IPostApiClient postApiClient, IHttpContextAccessor httpContextAccessor, ICategoryApiClient categoryApiClient, ITagApiClient tagApiClient)
		{
			_logger = logger;
			_postApiClient = postApiClient;
			_httpContextAccessor = httpContextAccessor;
			_categoryApiClient = categoryApiClient;
			_tagApiClient = tagApiClient;
		}

		public async Task<IActionResult> Index()
		{
		
			var PostList = await _postApiClient.RecentPost(1);
			ViewData["ObjectList"] = PostList;
			var Popular = await _postApiClient.TakeTopByQuantity(3);
			ViewData["PostPopular"] = Popular;
            var Trending = await _postApiClient.PostTrending(5);
            ViewData["PostTending"] = Trending;
            var PostRecent = await _postApiClient.RecentPost(3);
			ViewData["PostRecent"] = PostRecent;
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			var PostInDay = await _postApiClient.GetPostInDay();
			ViewData["PostInDay"] = PostInDay;
			var posts = await _postApiClient.GetAll();
			ViewData["posts"] = posts;
			var user = User.Identity.Name;
			var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

			if (user != null && sessions == null)
			{
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				HttpContext.Session.Remove("Token");

				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		public async Task<IActionResult> AboutUs()
		{
			var Category = await _categoryApiClient.GetAll();
			ViewData["Category"] = Category;
			return View();
		}
        public async Task<IActionResult> Contact()
        {
            var Category = await _categoryApiClient.GetAll();
            ViewData["Category"] = Category;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}