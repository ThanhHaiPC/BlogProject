using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Posts
{
    public class PostVm
    {
        public int PostID { get; set; }
        public Guid UserId { get; set; }
        public string Desprition { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? CountLike { get; set; }
        public int? CountComment { get; set; }
        public string Avatar { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadDate { get; set; }
        public int View { get; set; }
        public int CategoryId { get; set; }
        public int TagId { get; set; }
		public int? TagName { get; set; }
		public string? CategoryName { get; set; }
        public string? Image { get; set; }
        public bool Active { get; set; }
    }
}
