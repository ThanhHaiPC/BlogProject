using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Apilntegration.Users;
using BlogProject.Utilities.Constants;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using BlogProject.Apilntegration.Category;

namespace BlogProject.WebBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiPost;
        public AccountController(IUserApiClient userApiClient, IConfiguration configuration,ICategoryApiClient categoryApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _categoryApiPost = categoryApiClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

           
            var result = await _userApiClient.Authencate(request);
            if (result.ResultObj == null)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            var userPrincipal = this.ValidateToken(result.ResultObj);

            // Lưu cookie khi vào lại mà không logout
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = true
            };



            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

            var result = await _userApiClient.RegisterUser(registerRequest);
            if (!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            var loginResult = await _userApiClient.Authencate(new LoginRequest()
            {
                UserName = registerRequest.UserName,
                Password = registerRequest.Password,
                RememberMe = true
            });

            var userPrincipal = this.ValidateToken(loginResult.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, loginResult.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Session.Remove("Token");
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> AccountSetting(string UserName)
		{
			if (UserName != null)
			{
				var Category = await _categoryApiPost.GetAll();
				ViewData["Category"] = Category;
				var result = await _userApiClient.GetByUserName(UserName);
				if (TempData["result"] != null)
				{
					ViewBag.SuccessMsg = TempData["result"];
				}
				ViewBag.UserName = User.Identity.Name;
				return View(result.ResultObj);
			}
			else
			{
				var result = await _userApiClient.GetByUserName(User.Identity.Name);
				if (TempData["result"] != null)
				{
					ViewBag.SuccessMsg = TempData["result"];
				}
				ViewBag.UserName = User.Identity.Name;
				return View(result.ResultObj);
			}
		}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UpdateUserRequest()
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
        public async Task<IActionResult> Edit(UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UserUpdate(request, request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePass(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new ChangePassword()
                {
                    Id = id,
                };

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePassword request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var change = await _userApiClient.ChangePass(request, request.Id);
            if (change.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("AccountSetting");
            }
            ModelState.AddModelError("", change.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> ForgotPass(string email)
        {
            return View(new ForgotPassRequest()
            {
                Email = email
            });
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPass(ForgotPassRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var forgotpass = await _userApiClient.ForgotPass(request.Email);
            if(forgotpass.IsSuccessed)
            {
                TempData["result"] = "Một liên kết đặt lại mật khẩu đã được gửi đến địa chỉ email của bạn.";
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError(string.Empty, "Có lỗi xảy ra. Vui lòng thử lại sau.");
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> ResetPass(string token, string email)
        {
            var reset = new ResetPasswordViewModel()
            {
                Token = token,
                Email = email
            };

            return View(reset);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPass(ResetPasswordViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userApiClient.ResetPasswordAsync(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Đổi mật khẩu thành công";
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError(string.Empty, "Có lỗi xảy ra. Vui lòng thử lại sau.");
            return View(request);
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}
