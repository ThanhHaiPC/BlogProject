using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Catalog.RatingPost
{
    public class CreateRatingRequest
    {
        public Guid UserId { get; set; }
        public int PostId { get; set; }
        public int RatingValue { get; set; }
    }
}
