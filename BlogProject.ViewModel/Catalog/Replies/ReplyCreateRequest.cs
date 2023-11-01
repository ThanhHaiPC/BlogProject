using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Replies
{
    public class ReplyCreateRequest
    {

        public int ReplyID { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }

        public int? CommentID { get; set; }

        public DateTime Date { get; set; }
    }
}
