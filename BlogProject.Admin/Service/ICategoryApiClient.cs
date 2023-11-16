using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Categories;
using BlogProject.ViewModel.Common;

namespace BlogProject.Admin.Service
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll();
    }
}
