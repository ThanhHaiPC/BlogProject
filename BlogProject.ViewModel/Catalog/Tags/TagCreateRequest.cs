using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Tags
{
    public class TagCreateRequest
    {
        public int PostID { get; set; }
        public string TagName { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
