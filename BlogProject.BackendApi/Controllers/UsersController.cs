using BlogProject.Application.System.Users;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
		[HttpGet("Profile/{id}")]
		public async Task<IActionResult> Profile()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = await _userService.Profile(userId);
			return Ok(user);
		}
		[HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authencate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(request);

            if (!result.IsSuccessed)
            {
                return BadRequest("Username or password is incorrect.");
            }
            return Ok(result);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("setting/{username}/id")]
        public async Task<IActionResult> GetIdByUserName(string username)
        {
            var user = await _userService.GetIdByUserName(username);
            return Ok(user);
        }

        [HttpGet("paging")]
        
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var listuser = await _userService.GetUserPaging(request);
            return Ok(listuser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }
		[AllowAnonymous]
		[HttpGet("setting/{username}")]
		public async Task<IActionResult> GetByUserName(string username)
		{
			var user = await _userService.GetByUserName(username);
			return Ok(user);
		}
		[HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest request, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Update(request, id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("{id}/roles")]

        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(request, id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

		[HttpPost("follow")]
		public async Task<IActionResult> Follow([FromBody] FollowViewModel request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.AddFollow(request);
			if (!result.IsSuccessed)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}
		[HttpGet("pagingFollowers")]
		public async Task<IActionResult> GetAllFollowersPaging([FromQuery] GetUserPagingRequest request)
		{
			var products = await _userService.GetFollowersPaging(request);
			return Ok(products);
		}
        [HttpGet("CheckFollow")]
        public async Task<IActionResult> CheckFollow([FromQuery] FollowViewModel  request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userService.CheckFollow(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("user/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request, Guid id)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _userService.UpdateUser(request, id);
                if (!result.IsSuccessed)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
        }
        [HttpPut("changepass/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ChangePass([FromForm] ChangePassword request, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ChangePassword(request, id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("forgotpass/{email}")]
        public async Task<IActionResult> ForgotPass(string email)
        {
            var result = await _userService.ForgotPassword(email);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel request)
        {
            
                var result = await _userService.ResetPasswordAsync(request);

                if (result.IsSuccessed)
                    return Ok(result);

               return BadRequest(result);
            

            
        }
        [HttpPost("forgotpassadmin/{email}")]
        public async Task<IActionResult> ForgotPassAdmin(string email)
        {
            var result = await _userService.ForgotPasswordAdmin(email);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
