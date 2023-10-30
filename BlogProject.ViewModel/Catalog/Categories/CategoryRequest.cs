using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Categories
{
    public class CategoryRequest
    {
        public int CategoriesID { get; set; }
        [Display(Name = "Tên danh mục")]
        public string? Name { get; set; }
    }
}
