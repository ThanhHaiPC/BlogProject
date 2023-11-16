
using BlogProject.Data.EF;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _dbContext;

        public CategoryService(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<bool>> Create(CategoryRequest request)
        {
            if (request.Name == null)
            {
                return new ApiErrorResult<bool>("Tên danh mục trống");
            }
            List<Category> category = await _dbContext.Categories.Where(x => x.Name == request.Name).ToListAsync();


            if (category.Count != 0)
            {
                return new ApiErrorResult<bool>("danh mục đã tồn tại");
            }

            var category1 = new Category()
            {
                Name = request.Name
            };
            _dbContext.Categories.Add(category1);
            _dbContext.SaveChanges();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int categoryId)
        {
            if (categoryId == null)
            {
                return new ApiErrorResult<bool>("Lỗi cập nhập");
            }
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoriesID == categoryId);

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<CategoryVm>> GetAll()
        {
            var categories = await _dbContext.Categories
                .Select(x => new CategoryVm()
                {
                    id = x.CategoriesID,
                    name = x.Name,
                    
                }).ToListAsync();

            return categories;
        }

        public async Task<ApiResult<CategoryRequest>> GetById(int categoryId)
        {
            var cate = await _dbContext.Categories.Where(x => x.CategoriesID == categoryId).FirstOrDefaultAsync();
            if (cate == null)
            {
                return new ApiErrorResult<CategoryRequest>("Danh mục không tồn tại");
            }

            var category = new CategoryRequest()
            {
                CategoriesID = cate.CategoriesID,
                Name = cate.Name
            };
            return new ApiSuccessResult<CategoryRequest>(category);
        }

        public async Task<ApiResult<bool>> Update(int id, CategoryRequest request)
        {
            if (id == null)
            {
                return new ApiErrorResult<bool>("Lỗi cập nhập");
            }
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoriesID == id);

            category.Name = request.Name;

            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return new ApiSuccessResult<bool>();
        }
    }
}
