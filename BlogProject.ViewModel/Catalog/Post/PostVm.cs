using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Post
{
    public class PostVm
    {
        public int PostID { get; set; }
        public string Desprition { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Like { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadDate { get; set; }
        public int View { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
    }
}
