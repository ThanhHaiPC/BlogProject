using BlogProject.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Images
    {
        public int ImageID { get; set; }
        public int PostID { get; set; }
        public string? FileName { get; set; }
        public string Des { get; set; }
        public Active Active { get; set; }

        // Relationship
        public Posts Post { get; set; }
    }
}
