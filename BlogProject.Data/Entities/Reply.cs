using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Reply
    {
        public int ReplyID { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }

        public int? CommentID { get; set; }

        public DateTime Date { get; set; }

        // RelationShip
        public Comment Comment { get; set; }
        public AppUser User { get; set; }
    }
}
