using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Video
    {
        public int VideoID { get; set; }
        public int PostID { get; set; }

        public Guid UserId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string LinkURL { get; set; }
        public int OrderNo { get; set; }
        public DateTime Date { get; set; }
        // Relationship
        public Posts Post { get; set; }
        public AppUser User { get; set; }
    }
}
