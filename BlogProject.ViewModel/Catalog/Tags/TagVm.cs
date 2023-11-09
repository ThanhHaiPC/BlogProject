using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.Tags
{
    public class TagVm
    {
        public int TagId { get; set; }
      /*  public int? PostID { get; set; }*/
        public string TagName { get; set; }

        // You can add more properties as needed, depending on what information
        // you want to display in your view.

        // If you want to display the number of posts associated with a tag, you can add:
    /*    public int NumberOfPosts { get; set; }*/
        public int View {  get; set; }
    }
}
