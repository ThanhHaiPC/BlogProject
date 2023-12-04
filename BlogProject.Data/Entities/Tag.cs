using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public int View { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadDate { get; set; }


        // RelationShip
        public List<Posts> Post { get; set; }
       
    }
}
