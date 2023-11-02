using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Post
{
    public class PostRequest
    {

       
        public string Desprition { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile FileImage { get; set; }
        
    }
}
