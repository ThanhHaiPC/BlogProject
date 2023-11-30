using BlogProject.Data.Enum;
using Microsoft.AspNetCore.Http;
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
        public Guid Id { get; set; }

        [Display(Name = "Tên")]
        public string? FirstName { get; set; }
        [Display(Name = "Họ")]

        public string? LastName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Tên tài khoản")]
        public string? UserName { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string? Image { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string ImageFileName { get; set; }
        [Display(Name = "Giới tính")]
        public Gender? Gender { get; set; }
        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBir { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
