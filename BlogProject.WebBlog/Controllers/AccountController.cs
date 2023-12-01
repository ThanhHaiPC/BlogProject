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
