using BlogProject.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Posts
    {
        public int PostID { get; set; }
        public int ImageID { get; set; }
        public int AuthorID { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Desprition { get; set; }
        public Active Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadDate { get; set; }
        public int View { get; set; }
        public int OrderNo { get; set; }




        // relationship
        public User User { get; set; }     
        public List<CategoriesDetail> CategoriesDetail { get; set; }
        public List<Comment> Comment { get; set; }
        public List<Images> Images { get; set; }
        public List<Like> Likes { get; set; }
        public List<Video> Video { get; set; }
        public List<Tag> Tag { get; set; }
    }
}
