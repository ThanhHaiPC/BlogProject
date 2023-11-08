using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.RatingPost
{
    public class RatePostRequest
    {
        public int RatingID { get; set; }
        public int PostID { get; set; }
        public Guid UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }
}
