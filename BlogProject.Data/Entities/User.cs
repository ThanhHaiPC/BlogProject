using BlogProject.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        //   public string PhoneNumber { get; set; }
        public Gender? Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBir { get; set; }
        public string? Address { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime CreatedAt { get; set; }

		// relationship
		/*   public List<CategoriesDetail> CategoriesDetail { get; set; }*/
		public List<Comment> Comment { get; set; }
        public PasswordResetToken PasswordResetToken { get; set; }
        public List<Follow> Follower { get; set; }
        public List<Follow> Followee { get; set; }
        public List<Like> Like { get; set; }
        public List<Video> Video { get; set; }
        public List<Rating> Rating { get; set; }
        public List<Reply> Reply { get; set; }
        
    }
}
