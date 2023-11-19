using BlogProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Comments
{
    public class CommentVm
    {
        public int postId { get; set; }
        public Guid userId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        
    }
}
