using BlogProject.Application.Catalog.Post;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Like;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{

		private readonly IPostService _postService;

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;
		public PostsController(IPostService postService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			_postService = postService;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		[HttpGet("get-all")]
		public async Task<IActionResult> GetAll()
		{
			var posts = await _postService.GetAll();
			return Ok(posts);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPost([FromQuery]GetUserPagingRequest request)
		{

			var post = await _postService.GetPaged(request);
			return Ok(post);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromForm] PostRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var post = await _postService.Create(request, userId);
			if (!post.IsSuccessed)
				return BadRequest(post);

			return Ok(post);
		}
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(int Id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var post = await _postService.Delete(Id);
			if (!post.IsSuccessed)
				return BadRequest(post);
			return Ok(post);
		}
		[HttpPut("{Id}")]
		public async Task<IActionResult> Update([FromForm] PostUpdateRequest request, int Id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var post = await _postService.Update(request, Id);
			if (!post.IsSuccessed)
				return BadRequest(post);
			return Ok(post);
		}
		[HttpGet("get-all-paging")]
		public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
		{

			var post = await _postService.GetPagedAdmin(request);
			return Ok(post);
		}
		[HttpGet("get-by-user")]
		public async Task<IActionResult> GetByUserId(string userId)
		{


			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest("User ID not found.");
			}

			var posts = await _postService.GetByUserId(userId);
			return Ok(posts);
		}

		[HttpGet("search")]
		public async Task<IActionResult> Search([FromQuery] string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				return BadRequest("Search term is required.");
			}

			var posts = await _postService.Search(searchTerm);
			return Ok(posts);
		}
		[AllowAnonymous]
		[HttpGet("show/{quantity}")]
		public async Task<IActionResult> TakeByQuantity(int quantity)
		{
			var user = await _postService.TakeTopByQuantity(quantity);
			return Ok(user);
		}
        [HttpGet("post-trending/{quantity}")]
        public async Task<IActionResult> PostTrending(int quantity)
        {
            var user = await _postService.PostTrending(quantity);
            return Ok(user);
        }
        [HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var posts = await _postService.GetById(id);
			return Ok(posts);
		}
		[HttpGet("role")]
		[Authorize(Roles = "admin,author")]
		public async Task<IActionResult> GetByRole([FromQuery] GetUserPagingRequest request)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest("User ID not found.");
			}

			var articles = userId != null && User.IsInRole("author")
		   ? await _postService.GetByUserId(userId, request)
		   : await _postService.GetPagedAdmin(request);


			return Ok(articles);
		}
		[HttpGet("detail/{id}")]
		public async Task<IActionResult> Detail(int id)
		{
			var posts = await _postService.DetalUser(id);
			return Ok(posts);
		}
		/* [HttpGet("pagingallfollow")]
         public async Task<IActionResult> GetAllFollowPostPaging([FromQuery] GetUserPagingRequest request)
         {

             var products = await _postService.GetPostFollowPaging(request);
             return Ok(products);
         }*/



		[HttpGet("recent-post/{quatity}")]
		public async Task<IActionResult> RecentPost(int quatity)
		{
			var posts = await _postService.PostRecent(quatity);
			return Ok(posts);
		}
		[HttpGet("post-in-day")]
		public async Task<IActionResult> GetPostInDay()
		{
			var posts = await _postService.GetPostInDay();
			return Ok(posts);
		}

		[HttpGet("category/{categoryId}")]
		public async Task<IActionResult> GetPostOfCategory(int categoryId)
		{
			var posts = await _postService.GetPostOfCategory(categoryId);
			return Ok(posts);
		}
		[HttpPost("addlike")]
		public async Task<IActionResult> AddLike([FromBody] LikeVm request)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _postService.Like(request, userId);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);



		}
		[HttpGet("check")]
		public async Task<IActionResult> CheckLike([FromQuery] LikeVm request)
		{
			var result = await _postService.CheckLike(request.Username, request.Id);
			if (result != null)
			{
				return Ok();
			}

			return BadRequest();

		}
		[HttpPost("enable")]
		public async Task<IActionResult> Enable(PostEnable request)
		{
			var result = await _postService.Enable(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);

		}
		[HttpGet("history")]
		[Authorize]
		public async Task<IActionResult> HistoryLike(string userName)
		{

			var result = await _postService.History(userName);
			if (result == null)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}
		[HttpGet("check-enable")]
		public async Task<IActionResult> CheckEnable(int postId)
		{
			var result = await _postService.CheckEnable(postId);
			if(result == null)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}
	}
}