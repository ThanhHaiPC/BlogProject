using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class CategoriesDetail
    {
        public int CategoriesDetailID { get; set; }
        public int PostID { get; set; }
        public int CategoriesID { get; set; }
        public string Description { get; set; }

        // RelationShip
        public Posts Post { get; set; }
        public Category Category { get; set; }
    }
}
