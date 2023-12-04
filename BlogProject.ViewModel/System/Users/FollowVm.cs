using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.System.Users
{
	public class FollowVm
	{
		[Display(Name = "Tên tài khoản")]
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		[Display(Name = "Ảnh đại diện")]
		public string? Image { get; set; }
		public  DateTime DateTime { get; set; }
	}
}
