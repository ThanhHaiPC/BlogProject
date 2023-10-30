using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public int Like { get; set; }
        public DateTime Date { get; set; }

        // Relationship
        public Posts Post { get; set; }
        public User User { get; set; }
        public ICollection<Reply> Replies { get; set; }

    }
}
