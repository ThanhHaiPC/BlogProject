using BaseProject.AdminApp.Service;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaseProject.AdminApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserApiClient _userApiClient;
        public UsersController(IUserApiClient userApiClient,IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyword, int pageindex = 1, int pagesize= 10)
        {
			var session = HttpContext.Session.GetString("Token");
			var request = new GetUserPagingRequest()
			{
				BearerToken = session,
				Keyword = keyword,	
				PageIndex = pageindex,
				PageSize = pagesize
			};
			var data =await _userApiClient.GetUsersPagings(request);
            return View(data);
        }
        [HttpGet] 
        public async Task<IActionResult> Login()
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
			if (!ModelState.IsValid)
				return View(ModelState);

			var token = await _userApiClient.Authenticate(request);
			var userPrincipal = this.ValidateToken(token);
			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = false
			};
			
			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				userPrincipal,
				authProperties);
			HttpContext.Session.SetString("Token",token );
			return RedirectToAction("Index", "Home");
		}
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "User");
		}
		//Decoding function
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
