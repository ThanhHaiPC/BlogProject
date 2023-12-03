using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogProject.Apilntegration.Users;

namespace BlogProject.Admin.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var result = await _userApiClient.Authencate(request);

            var userPrincipal = this.ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            HttpContext.Session.SetString("Token", result.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
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
            var forgotpass = await _userApiClient.ForgotPassAdmin(request.Email);
            if (forgotpass.IsSuccessed)
            {
                TempData["result"] = "Một liên kết đặt lại mật khẩu đã được gửi đến địa chỉ email của bạn.";
                return RedirectToAction("Index", "Login");
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
                return RedirectToAction("Index", "Login");
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
