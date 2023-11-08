using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Replies
{
    public  class ReplyCreateRequest
    {
        public string Content { get; set; }

        public int? CommentID { get; set; }

        public DateTime Date { get; set; }
    }
}
