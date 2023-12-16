using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Videos
{
    public class VideoCreateRequest
    {
        public int? CategoryId {  get; set; }    
        public string? Title { get; set; }
        public string? Description { get; set; } 
        public string? FileUrl { get; set; }
        public IFormFile formFile { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? UpDate { get; set; }
    }
}
