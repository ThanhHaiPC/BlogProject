﻿using BlogProject.Application.Common;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogProject.Application.System.Users
{
    public class UserService : IUserService
    {
        private const string USER_CONTENT_FOLDER_NAME = "Images";
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly BlogDbContext _dataContext;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IStorageService _storageService;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            BlogDbContext dataContext, 
            IConfiguration config, 
            IHostingEnvironment hostingEnvironment
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dataContext = dataContext;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
           
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null ;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            // Xóa vai trò của người dùng
            var userRoles = await _userManager.GetRolesAsync(user);
            var resultRemoveRoles = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!resultRemoveRoles.Succeeded)
            {
                return new ApiErrorResult<bool>("Xóa không thành công");
            }

            // Bây giờ bạn có thể xóa người dùng
            var resultDeleteUser = await _userManager.DeleteAsync(user);
            var reult = await _userManager.DeleteAsync(user);
            if (!reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                DateOfBir = user.DateOfBir,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles,
                Image = user.Image,
                Gender = user.Gender,
                Address = user.Address,
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<Guid> GetIdByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.Id;
        }

        public async  Task<ApiResult<PagedResult<UserVm>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Id = x.Id,                 
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBir = x.DateOfBir,
                    Gender = x.Gender,
                    UserName = x.UserName,
                    Image =x.Image,
                    PhoneNumber = x.PhoneNumber,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
        }

        public async  Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }


           
            user = new User()
            {
                DateOfBir = request.DateOfBir,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                
                UserName = request.UserName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber
            };

            // Lưu mật khẩu
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // Auto RoleAssign
                var role = await _dataContext.Roles.FirstOrDefaultAsync(x => x.Name == "User");
                var getUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
                await _userManager.AddToRoleAsync(getUser, role.Name);
                return new ApiSuccessResult<bool>();
            }



            return new ApiErrorResult<bool>("Đăng ký không thành công : Mật khẩu không hợp lệ, yêu cầu gồm có ít 6 ký tự bao gồm ký tự: Hoa, thường, số, ký tự đặc biệt ");
        }

        public async Task<ApiResult<bool>> RoleAssign(RoleAssignRequest request, Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected == true).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(UserUpdateRequest request, Guid id)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (request.Dob != null)
            {
                user.DateOfBir = request.Dob;
            }
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.Gender = request.Gender;
            user.LastName = request.LastName;
            user.Address = request.Address;

            if (request.Image != null)
            {
                //if (user.Image != null)
                //{
                //    RemoveImage(user.Image);
                //}
                user.Image = await SaveFile(request.Image);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        //remove image
        //public async Task<bool> RemoveImage(string request)
        //{
        //    await _storageService.DeleteFileAsync(request);
        //    return true;
        //}
        //save image
        private async Task<string> SaveFile(IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            // Xác định đường dẫn tới thư mục lưu trữ hình ảnh trong thư mục gốc (wwwroot)
            string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

            // Tạo đường dẫn đầy đủ tới tệp hình ảnh
            string imagePath = Path.Combine(uploadPath, uniqueFileName);

            // Lưu tệp hình ảnh
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return "https://localhost:5001/"+ "/Images/" + uniqueFileName;
        }
    }
}
