using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Roles;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using BlogProject.ViewModel.Catalog.Categories;

namespace BlogProject.Admin.Service
{
    public class CategoryApiClient :BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<CategoryVm>> GetAll()
        {
            return await GetListAsync<CategoryVm>("api/Categories/getall");
        }
    }
}
