using BlogProject.Apilntegration.Posts;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostApiClient _postApiClient;
        public PostController(IPostApiClient postApiClient)
        {
            _postApiClient = postApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
			var sessions = HttpContext.Session.GetString("Token");
			var request = new GetUserPagingRequest()
			{

				Keyword = keyword,
				PageIndex = pageIndex,
				PageSize = pageSize
			};
			if (TempData["result"] != null)
			{
				ViewBag.SuccessMsg = TempData["result"];
			}
			var data = await _postApiClient.GetAllPaging(request);
			return View(data);
		}
    }
}
