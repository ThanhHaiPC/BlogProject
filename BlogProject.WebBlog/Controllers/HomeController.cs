using BlogProject.Apilntegration.Posts;
using BlogProject.WebBlog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;

namespace BlogProject.WebBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostApiClient _postApiClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger,IPostApiClient postApiClient, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _postApiClient = postApiClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            // Hàm để lấy danh sách địa điểm
            var PostList = await _postApiClient.TakeTopByQuantity(6);
            ViewData["ObjectList"] = PostList;


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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}