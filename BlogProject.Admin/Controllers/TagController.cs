
using BlogProject.Apilntegration.Posts;
using BlogProject.Apilntegration.Tags;
using BlogProject.Data.EF;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Catalog.Tags;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.Admin.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagApiClient _tagApiClient;
        private readonly IPostApiClient _postApiClient;
        public TagController(ITagApiClient tagApiClient,IPostApiClient postApiClient)
        {
            _tagApiClient = tagApiClient;
            _postApiClient = postApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)      
        {
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _tagApiClient.GetUsersPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Retrieve the list of posts
            var posts = await _tagApiClient.GetAll(); // Assuming there's a method to get all posts from the API client
            ViewBag.PostList = posts.Select(x => new SelectListItem()
            {
                Text = x.TagName, // Replace with the actual property representing the post title
                Value = x.TagId.ToString() // Replace with the actual property representing the post ID
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TagCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var post = await _tagApiClient.CreateTagByPost(request);
            if (post.IsSuccessed)
            {
                TempData["result"] = "Tạo Tag bài viết thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", post.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int tagId)
        {
            var result = await _tagApiClient.GetById(tagId);

            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
               
                var updateRequest = new TagVm()
                {
                    TagId = user.TagId,
                    TagName = user.TagName,
                    PostID = user.PostID,
                    UploadDate = user.UploadDate,
                    View = user.View

                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int  tagId, TagUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _tagApiClient.UpdateTag(tagId, request);
            if (result.IsSuccessed != null)
            {
                TempData["result"] = "Cập nhập tag bài viết  thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhập tag bài viết thất bại");
            return View();
        }


		[HttpGet]
		public async Task<IActionResult> Delete(int tagId)
		{
			return View(new TagDeleteRequest()
			{
                Id = tagId
            });
		}

		[HttpPost]
		public async Task<IActionResult> Delete(TagDeleteRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var result = await _tagApiClient.DeleteTag(request.Id);
			if (result.IsSuccessed)
			{
				TempData["result"] = "Xóa tag bài viết thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			return View(request);
		}
	}
}
