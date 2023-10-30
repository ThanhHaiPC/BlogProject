﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Category
    {
        public int CategoriesID { get; set; }
        public string Name { get; set; }
        // Relationship
        public List<CategoriesDetail>? CategoriesDetail { get; set; }
    }
}
