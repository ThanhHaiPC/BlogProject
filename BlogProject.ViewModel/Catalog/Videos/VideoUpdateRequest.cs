using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Videos
{
    public class VideoUpdateRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? LinkUrl { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageFileName { get; set; }

    }
}
