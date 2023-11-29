using BlogProject.ViewModel.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Posts
{
    public class PostDetailRequest
    {
        public PostVm Post { get; set; }
        public List<CommentVm> Comments { get; set; }
    }
}
