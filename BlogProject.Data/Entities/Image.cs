using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public int? PostId { get; set; }
        public string? FileName { get; set; }
        public string Path { get; set; }

        // Relationship
        public Posts Posts { get; set; }
    }
}
