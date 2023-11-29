using BlogProject.Apilntegration.Category;
using BlogProject.Apilntegration.Posts;
using BlogProject.Apilntegration.Tags;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebBlog.Controllers
{
	public class TagController : Controller
	{

		private readonly IHttpContextAccessor _contextAccessor;
		private readonly ITagApiClient _tagApiClient;
		private readonly IPostApiClient _postApiClient;
		private	readonly ICategoryApiClient _categoryApiClient;
		public TagController(IHttpContextAccessor contextAccessor, 
			ITagApiClient tagApiClient, 
			IPostApiClient postApiClient,
			ICategoryApiClient categoryApiClient)
		{
			_contextAccessor = contextAccessor;
			_tagApiClient = tagApiClient;
			_postApiClient = postApiClient;
			_categoryApiClient = categoryApiClient;
		}

		[HttpGet]
		public async Task<IActionResult> ListPostOfTag(int PostId)
		{
			var tags = await _tagApiClient.GetAll();
			ViewData["Tag"] = tags;
            var Category = await _categoryApiClient.GetAll();
            ViewData["Category"] = Category;
            var post = await _tagApiClient.ListPostsForTag(PostId);

			return View(post);
		}
	}
}
