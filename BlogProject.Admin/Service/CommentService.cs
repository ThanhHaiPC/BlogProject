using BlogProject.ViewModel.Catalog.Comments;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogProject.Admin.Service
{
    public class CommentService : BaseApiClient, ICommentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public CommentService(IHttpClientFactory httpClientFactory,
                    IHttpContextAccessor httpContextAccessor,
                     IConfiguration configuration)
             : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedResult<CommentVm>> GetByPostId(int postId, GetUserPagingRequest request)
        {
            return await GetAsync < PagedResult < CommentVm >> ($"/api/Comment/comments/{postId}?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            
        }
    }
}
