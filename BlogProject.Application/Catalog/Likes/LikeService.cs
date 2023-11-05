using BlogProject.Application.Common;
using BlogProject.Application.System.Users;
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Likes
{
    public class LikeService : ILikeService
    {
        private readonly BlogDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IUserService _userService;

        public LikeService(BlogDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async  Task<int> CountAsyncById(int id)
        {
            return await _context.Likes.Where(x => x.PostID == id).CountAsync();
        }

        public int CountById(int id)
        {
            return _context.Likes.Where(x => x.PostID == id).Count();
        }
    }
}
