using BlogProject.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.System.Users
{
	public class UserVm
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }
		public string? Image { get; set; }
		
		public string PhoneNumber { get; set; }
		public Gender? Gender { get; set; }
		public string Email { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfBir { get; set; }
		public string? Address { get; set; }

	}
}
