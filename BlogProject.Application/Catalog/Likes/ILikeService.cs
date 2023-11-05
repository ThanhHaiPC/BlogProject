using BlogProject.Data.Entities;
using BlogProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Catalog.Likes
{
    public interface ILikeService
    {
        int CountById(int id);
        Task<int> CountAsyncById(int id);

    }
}
