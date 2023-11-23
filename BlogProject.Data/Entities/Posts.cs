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

        public int CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Desprition { get; set; }


        public Active Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadDate { get; set; }
        public int View { get; set; }
        public int Like { get; set; }    

    

        public int OrderNo { get; set; }
        public string Image { get; set; }



        // relationship
        public User User { get; set; }
        public Category Categories { get; set; }
        public List<Comment> Comment { get; set; }

        public List<Like> Likes { get; set; }
        public List<Video> Video { get; set; }
        public List<Tag> Tag { get; set; }
        

        public Posts(Guid userId, string title)
        {
            Title = title;
            UserId = userId;
            UploadDate = DateTime.Now;
            View = 0;
        }
        public Posts(Posts obj)
        {
            this.PostID = obj.PostID;
            this.View = obj.View;
            this.UploadDate = DateTime.Now;
            this.UserId = obj.UserId;
            this.Title = obj.Title;
        }
        public Posts()
        {

        }
    }
}