using BlogProject.Admin.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Admin.Controllers
{
    [Authorize(Roles ="admin,author")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostApiClient _postApiClient;
        private readonly IConfiguration _configuration;

        public CommentController(ICommentService commentService, IConfiguration configuration, IPostApiClient postApiClient)
        {
            _commentService = commentService;
            _configuration = configuration;
            _postApiClient = postApiClient;
        }
        
    }
}
