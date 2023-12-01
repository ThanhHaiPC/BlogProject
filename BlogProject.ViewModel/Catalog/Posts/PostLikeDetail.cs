using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogProject.Data.Entities;
using BlogProject.ViewModel.Catalog.Like;

namespace BlogProject.ViewModel.Catalog.Posts
{
    public class PostLikeDetail
    {
        List<PostVm> Posts { get; set; }    
        LikeVm Like { get; set; }
    }
}
