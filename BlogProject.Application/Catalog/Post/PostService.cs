using BlogProject.Application.Catalog.Categories;
using BlogProject.Application.Catalog.Comments;
using BlogProject.Application.Catalog.Likes;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.Data.Enum;
using BlogProject.ViewModel.Catalog.Like;
using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogProject.Application.Catalog.Post
{

	public class PostService : IPostService
	{
		private readonly BlogDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly IUserService _userService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		/*    private readonly ICommentService _commentService;*/
		private readonly ILikeService _likeService;

		public PostService(BlogDbContext context, UserManager<User> userManager, IUserService userService, IWebHostEnvironment webHostEnvironment/*, ICommentService commentService*/, ILikeService likeService)
		{
			_context = context;
			_userManager = userManager;
			_webHostEnvironment = webHostEnvironment;
			/* _commentService = commentService;*/
			_likeService = likeService;
			_userService = userService;

		}

		public async Task<ApiResult<bool>> Create(PostRequest request, string userId)
		{
			if (request.Title == null)
			{
				return new ApiErrorResult<bool>("Tiêu đề trống");
			}
			if (request.Desprition == null)
			{
				return new ApiErrorResult<bool>("Mô tả trống");
			}
			if (request.Content == null)
			{
				return new ApiErrorResult<bool>("Chưa nhập nội dung");
			}
			/* if (request.CategoryId == null || request.CategoryId <= 0)
             {
                 return new ApiErrorResult<bool>("CategoryId không hợp lệ");
             }*/

			// Check if the specified CategoryId exists in the database
			var categoryExists = await _context.Categories.AnyAsync(c => c.CategoriesID == request.CategoryId);
			if (!categoryExists)
			{
				return new ApiErrorResult<bool>("CategoryId không tồn tại");
			}

			List<Posts> posts = await _context.Posts.Where(x => x.Title == request.Title).ToListAsync();
			if (posts.Count != 0)
			{
				return new ApiErrorResult<bool>("Bài viết đã tồn tại");
			}


			var user = await _userManager.FindByIdAsync(userId);
			// Tạo một đối tượng Post từ dữ liệu PostViewModel
			var add = new Posts
			{

				Title = request.Title,
				Content = request.Content,
				UserId = user.Id, // Thiết lập UserId dựa trên thông tin đăng nhập
				UploadDate = DateTime.Now,
				Desprition = request.Desprition,
				CategoryId = request.CategoryId,
				Image = await SaveFile(request.Image),
				Active = Active.yes

			};


			_context.Posts.Add(add);
			await _context.SaveChangesAsync();

			return new ApiSuccessResult<bool>();
		}

		public async Task<ApiResult<bool>> Delete(int Id)
		{
			var post = await _context.Posts.FirstOrDefaultAsync(a => a.PostID == Id);
			_context.Posts.Remove(post);
			await _context.SaveChangesAsync();
			return new ApiSuccessResult<bool>();
		}

		public async Task<ApiResult<List<Posts>>> GetAll()
		{
			var posts = await _context.Posts.Include(p => p.User).ToListAsync();


			return new ApiSuccessResult<List<Posts>>(posts);
		}


        public async Task<List<PostVm>> GetByUserId(string userId)
        {
			
                var posts = await _context.Posts
             .Where(p => p.UserId == Guid.Parse(userId) && p.Active == Active.yes)
             .Include(p => p.User)
          
             .Include(p => p.Categories)
             .ToListAsync();
                var postsWithUsernames = posts.Select(post => new PostVm
				{
					PostID = post.PostID,
					UserName = post.User.UserName,
					Title = post.Title,
					Content = post.Content,
					UploadDate = post.UploadDate,
					View = post.View,
					CategoryId = post.CategoryId,
					Desprition = post.Desprition,
					Image = post.Image,
					CategoryName = post.Categories.Name
				}).ToList();

                return (postsWithUsernames);
            
		}

		public async Task<ApiResult<List<PostVm>>> Search(string searchTerm)
		{
			try
			{
				var posts = await _context.Posts
					.Where(p => p.Title.Contains(searchTerm) || p.Desprition.Contains(searchTerm) && p.Active == Active.yes)
					.Include(p => p.User)
					.Include(p => p.Categories)
					.ToListAsync();

				var postsWithUsernames = posts.Select(post => new PostVm
				{
					PostID = post.PostID,
					UserName = post.User.UserName,
					Title = post.Title,
					Content = post.Content,
					UploadDate = post.UploadDate,
					View = post.View,
					CategoryId = post.CategoryId,
					Desprition = post.Desprition,
					Image = post.Image,
				}).ToList();

				return new ApiSuccessResult<List<PostVm>>(postsWithUsernames);
			}
			catch (Exception ex)
			{
				return new ApiErrorResult<List<PostVm>>($"An error occurred while searching for posts: {ex.Message}");
			}
		}

		public async Task<PagedResult<PostVm>> GetPaged(GetUserPagingRequest request)
		{
			var query = from p in _context.Posts
						select new { p };

			if (!string.IsNullOrEmpty(request.Keyword))
			{
				query = query.Where(x => x.p.Title.Contains(request.Keyword) || x.p.Desprition.Contains(request.Keyword) && x.p.Active == Active.yes);
			}
            else
            {
                query = query.Where(x => x.p.Active == Active.yes);
            }
            
            int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				
				.Select(x => new PostVm()
				{

					Title = x.p.Title,
					Content = x.p.Content,
					UserName = x.p.User.UserName,
					Desprition = x.p.Desprition,
					Image = x.p.Image,
					View = x.p.View,
					UploadDate = x.p.UploadDate,
					CategoryName = x.p.Categories.Name,
					PostID = x.p.PostID,
					CountComment = _context.Comments
					   .Where(c => c.PostID == x.p.PostID)
					   .Count(),
					CountLike = _context.Likes
						.Where(c => c.PostID == x.p.PostID)
						.Count()
				}).ToListAsync();
			//4. Select and projection
			var pagedResult = new PagedResult<PostVm>()
			{
				TotalRecords = totalRow,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				Items = data
			};
			return pagedResult;
		}

		public async Task<ApiResult<bool>> Update(PostUpdateRequest request, int id)
		{
			if (id == null)
			{
				return new ApiErrorResult<bool>("Lỗi cập nhập");
			}
			var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id);
			var categories = _context.Categories.ToList(); // Thay Categories bằng tên thật của bảng Category



			post.Desprition = request.Desprition;
			post.Title = request.Title;
			post.Content = request.Content;
			post.CategoryId = (int)request.CategoryId;
			if (request.Image != null)
			{

				post.Image = await SaveFile(request.Image);
			}

			_context.Posts.Update(post);
			_context.SaveChanges();
			return new ApiSuccessResult<bool>(true);
		}

		public async Task<List<PostVm>> TakeTopByQuantity(int quantity)
		{
			if (_context.Posts.Include(c => c.User).Include(c => c.Categories).Where(x => x.Active == Data.Enum.Active.yes).ToList().Count < quantity) { quantity = _context.Posts.ToList().Count; }
			var post = await _context.Posts
			.OrderByDescending(p => p.View)
			.Where(x=>x.Active == Active.yes)
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Take(quantity)
			.ToListAsync();

			List<PostVm> postList = new List<PostVm>();
			foreach (var item in post)
			{
				PostVm postVm = new PostVm();
				postVm.Title = item.Title;
				postVm.UserId = item.UserId;
				postVm.PostID = item.PostID;
				postVm.UploadDate = item.UploadDate;
				postVm.View = item.View;
				postVm.Content = item.Content;
				postVm.UserName = item.User.UserName;
				postVm.Image = item.Image;
				postVm.CategoryName = item.Categories.Name;

				postList.Add(postVm);
			}

			return postList;
		}

		public async Task<ApiResult<PostVm>> GetById(int Id)
		{
			var postId = await _context.Posts
				.Include(p => p.Categories)
				.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PostID == Id);

			var postvm = new PostVm()
			{
				Title = postId.Title,
				Desprition = postId.Desprition,
				Content = postId.Content,
				UserName = postId.User.UserName,
				CategoryName = postId.Categories.Name,
				Image = postId.Image,
				UploadDate = postId.UploadDate,
				UserId = postId.UserId,
				PostID = postId.PostID,
				Avatar = postId.User.Image,
				View = postId.View,

				CountComment = _context.Comments
					   .Where(c => c.PostID == postId.PostID)
					   .Count(),
				CountLike = _context.Likes
					   .Where(c => c.PostID == postId.PostID)
					   .Count(),
			};
			_context.SaveChanges();
			return new ApiSuccessResult<PostVm>(postvm);

		}
		//User's Detail
		public async Task<ApiResult<PostVm>> DetalUser(int Id)
		{
			var postId = await _context.Posts
				.Include(p => p.Categories)
				.Include(p => p.User)
                .Where(x => x.Active == Active.yes)
                .FirstOrDefaultAsync(p => p.PostID == Id);

			var postvm = new PostVm()
			{
				Title = postId.Title,
				Desprition = postId.Desprition,
				Content = postId.Content,
				UserName = postId.User.UserName,
				CategoryName = postId.Categories.Name,
				Image = postId.Image,
				UploadDate = postId.UploadDate,
				UserId = postId.UserId,
				PostID = postId.PostID,
				Avatar = postId.User.Image,
				View = postId.View++,
				CategoryId = postId.CategoryId,
				CountComment = _context.Comments
					   .Where(c => c.PostID == postId.PostID)
					   .Count(),
				CountLike = _context.Likes
					   .Where(c => c.PostID == postId.PostID)
					   .Count(),
			};
			_context.SaveChanges();
			return new ApiSuccessResult<PostVm>(postvm);

		}
		/*   public async Task<ApiResult<PagedResult<PostVm>>> GetPostFollowPaging(GetUserPagingRequest request)
           {
               try
               {
                   var userId = await _userService.GetIdByUserName(request.Name);

                   // Get the list of users the specified user is following
                   var followings = await _context.Follows
                       .Where(x => x.FolloweeId == userId)
                       .ToListAsync();

                   // Get all posts
                   var posts = await _context.Posts
                       .Where(post => followings.Any(user => user.FollowerId == post.UserId) && post.Active == 0)
                       .OrderByDescending(post => post.PostID)
                       .ToListAsync();

                   // Paging
                   int totalRow = posts.Count();

                   var data = posts
                       .Skip((request.PageIndex - 1) * request.PageSize)
                       .Take(request.PageSize)
                       .Select(post => new PostVm()
                       {
                           PostID = post.PostID,
                           Title = post.Title,
                           UploadDate = post.UploadDate,
                         *//*  CountComment = _commentService.CountById(post.PostID),*//*
                           CountLike = _likeService.CountById(post.PostID),
                           UserName = _userService.GetUserNameById(post.UserId),
                           View = post.View
                       })
                       .ToList();

                   // Select and projection
                   var pagedResult = new PagedResult<PostVm>()
                   {
                       TotalRecords = totalRow,
                       PageIndex = request.PageIndex,
                       PageSize = request.PageSize,
                       Items = data
                   };

                   return new ApiSuccessResult<PagedResult<PostVm>>(pagedResult);
               }
               catch (Exception ex)
               {
                   return new ApiErrorResult<PagedResult<PostVm>>($"An error occurred while getting paged posts: {ex.Message}");
               }
           }*/

		//save image
		private async Task<string> SaveFile(IFormFile file)
		{
			string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

			// Xác định đường dẫn tới thư mục lưu trữ hình ảnh trong thư mục gốc (wwwroot)
			string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

			// Tạo đường dẫn đầy đủ tới tệp hình ảnh
			string imagePath = Path.Combine(uploadPath, uniqueFileName);

			// Lưu tệp hình ảnh
			using (var stream = new FileStream(imagePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			return "https://localhost:5001/" + "/Images/" + uniqueFileName;
		}

		public async Task<PagedResult<PostVm>> GetByUserId(string userId, GetUserPagingRequest request)
		{
			var posts = await _context.Posts
					 .Where(p => p.UserId == Guid.Parse(userId) && p.Active == Active.yes)
					 .Include(p => p.User)
           
					 .Include(p => p.Categories)
                     .ToListAsync();
			if (!string.IsNullOrEmpty(request.Keyword))
			{
				posts = (List<Posts>)posts.Where(x => x.Title.Contains(request.Keyword) || x.Desprition.Contains(request.Keyword));
			}
			int totalRow = posts.Count();
			var postsWithUsernames = posts.Select(post => new PostVm
			{
				PostID = post.PostID,
				UserName = post.User.UserName,
				Title = post.Title,
				Content = post.Content,
				UploadDate = post.UploadDate,
				View = post.View,
				CategoryId = post.CategoryId,
				CategoryName = post.Categories.Name,
				Desprition = post.Desprition,
				Image = post.Image,
				CountComment = _context.Comments
					   .Where(c => c.PostID == post.PostID)
					   .Count(),
			}).ToList();

			var pagedResult = new PagedResult<PostVm>()
			{
				TotalRecords = totalRow,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				Items = postsWithUsernames
			};
			return pagedResult;
		}



		public async Task<List<PostVm>> PostRecent(int quantity)
		{
			if (_context.Posts.Include(c => c.User).Include(c => c.Categories).Where(x => x.Active == Active.yes).ToList().Count < quantity) { quantity = _context.Posts.ToList().Count; }
			var post = await _context.Posts
			.OrderByDescending(p => p.UploadDate)
            .Where(x => x.Active == Active.yes)
			.Include(a=>a.User )
			.Include(a=> a.Categories)
            .Take(quantity)
			.ToListAsync();

			List<PostVm> postList = new List<PostVm>();
			foreach (var item in post)
			{
				PostVm postVm = new PostVm();
				postVm.Title = item.Title;
				postVm.UserId = item.UserId;
				postVm.PostID = item.PostID;
				postVm.UploadDate = item.UploadDate;
				postVm.View = item.View;
				postVm.Content = item.Content;
				postVm.UserName = item.User.UserName;
				postVm.Image = item.Image;
				postVm.CategoryName = item.Categories.Name;

				postList.Add(postVm);
			}

			return postList;
		}

		public async Task<List<PostVm>> GetPostInDay()
		{
			DateTime today = DateTime.Today;

			var post = await _context.Posts
			.Where(p => p.UploadDate.Date == today && p.Active == Active.yes)
			.Include(p => p.Categories)
			.Include(p => p.User)
			.ToListAsync();

			List<PostVm> postList = new List<PostVm>();
			foreach (var item in post)
			{
				PostVm postVm = new PostVm();
				postVm.Title = item.Title;
				postVm.UserId = item.UserId;
				postVm.PostID = item.PostID;
				postVm.UploadDate = item.UploadDate;
				postVm.View = item.View;
				postVm.Content = item.Content;
				postVm.UserName = item.User.UserName;
				postVm.Image = item.Image;
				postVm.CategoryName = item.Categories.Name;

				postList.Add(postVm);
			}

			return postList;
		}

		public async Task<List<PostVm>> GetPostOfCategory(int categoryId)
		{
			var post = await _context.Posts
				.Include(c => c.Categories)
				.Include(x => x.User)
				.Where(x => x.CategoryId == categoryId && x.Active ==Active.yes)
				.ToListAsync();

			List<PostVm> postVms = new List<PostVm>();
			foreach (var item in post)
			{
				PostVm postVm = new PostVm();
				postVm.Title = item.Title;
				postVm.Desprition = item.Desprition;
				postVm.Image = item.Image;
				postVm.Avatar = item.User.Image;
				postVm.CategoryName = item.Categories.Name;
				postVm.UploadDate = item.UploadDate;
				postVm.UserName = item.User.UserName;
				postVm.CategoryId = item.CategoryId;
				postVm.PostID = item.PostID;
				
				postVms.Add(postVm);
			}
			return postVms;
		}

		public async Task<ApiResult<bool>> Like(LikeVm request, string userId)
		{
			var userID = await _userManager.FindByIdAsync(userId);
			var existingLike = _context.Likes.FirstOrDefault(l => l.PostID == request.Id && l.UserId == userID.Id);
			if (existingLike != null)
			{
				_context.Likes.Remove(existingLike);
				await _context.SaveChangesAsync();

				return new ApiSuccessResult<bool>();
			}
			else
			{
				var addLikes = new Like();
				addLikes.UserId = userID.Id;
				addLikes.PostID = request.Id;
				addLikes.Date = DateTime.Now;

				_context.Likes.Add(addLikes);
				await _context.SaveChangesAsync();
			}

			return new ApiSuccessResult<bool>();
		}

		public async Task<Like> CheckLike(string UserName, int Id)
		{
			var user = await _userService.GetIdByUserName(UserName);
			var check = await _context.Likes.FirstOrDefaultAsync(x => x.PostID == Id && x.UserId == user);
			return check;
		}

        public async Task<ApiResult<bool>> Enable(PostEnable request)
        {
            int check = int.Parse(request.Number);
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == request.IdPost);

            if (post != null)
            {
                if (check == 0 && post.Active == Active.yes)
                {
                    post.Active = Active.no;
                }
                else if (check == 1 && post.Active == Active.no)
                {
                    post.Active = Active.yes;
                }


                _context.Update(post);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Post not found");
            }
        }

        public async Task<List<Posts>> History(string userName)
        {
            var likedPosts =await _context.Likes
           .Where(like => like.User.UserName == userName)
           .Select(like => like.Post)
           .ToListAsync();

            return likedPosts;
        }

        public async Task<PagedResult<PostVm>> GetPagedAdmin(GetUserPagingRequest request)
        {
            var query = from p in _context.Posts
                        select new { p };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Title.Contains(request.Keyword) || x.p.Desprition.Contains(request.Keyword));
            }
            

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)

                .Select(x => new PostVm()
                {

                    Title = x.p.Title,
                    Content = x.p.Content,
                    UserName = x.p.User.UserName,
                    Desprition = x.p.Desprition,
                    Image = x.p.Image,
                    View = x.p.View,
                    UploadDate = x.p.UploadDate,
                    CategoryName = x.p.Categories.Name,
                    PostID = x.p.PostID,
					Active = x.p.Active,
                    CountComment = _context.Comments
                       .Where(c => c.PostID == x.p.PostID)
                       .Count(),
                    CountLike = _context.Likes
                        .Where(c => c.PostID == x.p.PostID)
                        .Count()
                }).ToListAsync();
            //4. Select and projection
            var pagedResult = new PagedResult<PostVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> CheckEnable(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            
            if (post != null && post.Active == Active.yes)
            {
                return new ApiSuccessResult<bool>(); 
            }

            return new ApiErrorResult<bool>();
        }
    }

}