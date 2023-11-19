using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Posts
{
    public class PostRequest
    {
        public int CategoryId { get; set; }
        public string Desprition { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageFileName { get; set; }
    }
}
