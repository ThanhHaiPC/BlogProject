using BlogProject.Apilntegration.Category;
using BlogProject.Apilntegration.Posts;
using BlogProject.Data.EF;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace BlogProject.Admin.Controllers
{
    public class PostController : Controller
    {     
            private readonly IPostApiClient _postApiClient;
            private readonly IConfiguration _configuration;
            private readonly ICategoryApiClient _categoryApiClient;
            private readonly BlogDbContext _context;
            public PostController(IPostApiClient postApiClient, IConfiguration configuration, ICategoryApiClient categoryApiClient, BlogDbContext context)
            {
                _postApiClient = postApiClient;
                _configuration = configuration;
                _categoryApiClient = categoryApiClient;
                _context = context;
            }

            public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 1)
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
                var data = await _postApiClient.GetPagings(request);
                return View(data);
            }
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var result = await _postApiClient.GetById(id);

                var categories = await _categoryApiClient.GetAll();
                ViewBag.CategoryList = categories.Select(x => new SelectListItem()
                {
                    Text = x.name,
                    Value = x.id.ToString()
                });

                if (result.IsSuccessed)
                {
                    var post = result.ResultObj;
                    var updateRequest = new PostUpdateRequest()
                    {
                        Title = post.Title,
                        Desprition = post.Desprition,
                        Content = post.Content,
                        ImageFileName = post.Image,
                        CategoryId = post.CategoryId,
                    };
                    return View(updateRequest);
                }
                return RedirectToAction("Error", "Home");
            }
            [HttpPost]
            public async Task<IActionResult> Edit(PostUpdateRequest request)
            {
                if (!ModelState.IsValid)
                    return View();

                var result = await _postApiClient.UpdatePost(request, request.Id);
                if (result.IsSuccessed)
                {
                    TempData["result"] = "Cập nhật bài viết thành công";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
                return View(request);
            }

            [HttpGet]
            public async Task<IActionResult> Create()
            {
                var categories = await _categoryApiClient.GetAll();
                ViewBag.CategoryList = categories.Select(x => new SelectListItem()
                {
                    Text = x.name,
                    Value = x.id.ToString()
                });
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(PostRequest request)
            {
                if (!ModelState.IsValid)
                    return View(request);

                var post = await _postApiClient.CreatePost(request);
                if (post.IsSuccessed)
                {
                    TempData["result"] = "Tạo bào viết thành công";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", post.Message);
                return View(request);
            }
            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                return View(new PostDeleteRequest()
                {
                    Id = id
                });
            }
            [HttpPost]
            public async Task<IActionResult> Delete(PostDeleteRequest request)
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }
                var post = await _postApiClient.DeletePost(request.Id);
                if (post.IsSuccessed)
                {
                    TempData["result"] = "Tạo bào viết thành công";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", post.Message);
                return View(request);
            }
            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var post = await _postApiClient.GetById(id);
                return View(post.ResultObj);
            }
        }
}