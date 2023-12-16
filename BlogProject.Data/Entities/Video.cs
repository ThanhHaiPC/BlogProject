using BlogProject.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Video
    {
        public int VideoID { get; set; }
        public Guid UserId { get; set; }   
        public string Title { get; set; }

        public string Description { get; set; }
        public int View { get; set; }

        
        public string ImageUrl { get; set; }
        public string LinkURL { get; set; }
        public Active Active { get; set; }
        public DateTime UpDate { get; set; }
        // Relationship
        public List<Comment> Comment { get; set; }
        public List<Like> Likes { get; set; }
        public Category Categories { get; set; }
        public Posts Post { get; set; }
        public User User { get; set; }
    }
}
