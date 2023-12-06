using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Authentication;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.Apilntegration.Users;
using BlogProject.Apilntegration.Roles;
using Microsoft.AspNetCore.Authorization;

namespace BlogProject.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _roleApiClient;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration, IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }
		[Authorize(Roles = "admin")]
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
			var data = await _userApiClient.GetUserAuthor(request);
			return View(data.ResultObj);
		}
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> ListUser(string keyword, int pageIndex = 1, int pageSize = 5)
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
			var data = await _userApiClient.GetUserUser(request);
			return View(data.ResultObj);
		}
		[HttpGet]
		[Authorize(Roles = "admin")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Create(RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return View();

			var result = await _userApiClient.RegisterUser(request);
			if (result.IsSuccessed)
			{
				TempData["result"] = "Thêm mới người dùng thành công";
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", result.Message);

			return View(request);
		}
		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Details(Guid id)
		{
			var user = await _userApiClient.GetById(id);
			return View(user.ResultObj);
		}
		[HttpGet]
		//[Authorize(Roles = "admin,author")]
		public async Task<IActionResult> Edit(Guid id)
		{


			var result = await _userApiClient.GetById(id);
			if (result.IsSuccessed)
			{
				var user = result.ResultObj;
				var updateRequest = new UserUpdateRequest()
				{
					Id = id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					ImageFileName = user.Image,
					Email = user.Email,
					Dob = user.DateOfBir,
					Gender = user.Gender,
					PhoneNumber = user.PhoneNumber,
					Address = user.Address,
				};
				return View(updateRequest);
			}
			return RedirectToAction("Error", "Home");


		}
		[HttpPost]
		//[Authorize(Roles = "admin,author")]
		public async Task<IActionResult> Edit(UserUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return View();

			var result = await _userApiClient.UpdateUser(request.Id, request);
			if (result.IsSuccessed)
			{
				TempData["result"] = "Cập nhật người dùng thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			return View(request);
		}
		[HttpPost]

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Session.Remove("Token");
			return RedirectToAction("Index", "Login");
		}
		[HttpGet]
		[Authorize(Roles = "admin")]
		public IActionResult Delete(Guid id)
		{
			return View(new UserDeleteRequest()
			{
				Id = id
			});
		}
		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(UserDeleteRequest request)
		{
			if (!ModelState.IsValid)
				return View();

			var result = await _userApiClient.DeleteUser(request.Id);
			if (result.IsSuccessed)
			{
				TempData["result"] = "Xóa người dùng thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			return View(request);
		}
		[HttpGet]
		//[Authorize(Roles = "admin")]
		public async Task<IActionResult> RoleAssign(Guid id)
		{
			var roleAssignRequest = await GetRoleAssignRequest(id);
			return View(roleAssignRequest);
		}

		[HttpPost]
		//[Authorize(Roles = "admin")]
		public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
		{
			if (!ModelState.IsValid)
				return View();

			var result = await _userApiClient.RoleAssign(request.Id, request);

			if (result.IsSuccessed)
			{
				TempData["result"] = "Cập nhật quyền thành công";
				return RedirectToAction("Index");
			}

			ModelState.AddModelError("", result.Message);
			var roleAssignRequest = await GetRoleAssignRequest(request.Id);

			return View(roleAssignRequest);
		}
		[HttpGet]
		[Authorize(Roles = "admin,author")]
		public async Task<IActionResult> Profile(Guid id)
		{

			var profile = await _userApiClient.Profile(id);
			return View(profile.ResultObj);
		}
		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> GetMonthlyStats()
		{
			return View();

		}

		[HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMonthlyStats(int? month, int? year,string keyword, int pageIndex = 1, int pageSize = 5)
		{
            var request = new GetUserPagingRequest()
            {

                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            ViewBag.SelectedMonth = month;
            ViewBag.SelectedYear = year;
            ViewBag.Keyword = keyword;
         
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            var data = await _userApiClient.GetMonthlyStats(request, month, year);
            
            return PartialView("_UserListPartial", data.ResultObj); // Trả về một partial view
            

        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
		{
			var userObj = await _userApiClient.GetById(id);
			var roleObj = await _roleApiClient.GetAll();
			var roleAssignRequest = new RoleAssignRequest();
			foreach (var role in roleObj.ResultObj)
			{
				roleAssignRequest.Roles.Add(new SelectItem()
				{
					Id = role.Id.ToString(),
					Name = role.Name,
					Selected = userObj.ResultObj.Roles.Contains(role.Name)
				});
			}
			return roleAssignRequest;
		}


	}
}