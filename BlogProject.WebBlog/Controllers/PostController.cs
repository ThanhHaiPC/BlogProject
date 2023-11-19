using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebBlog.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
