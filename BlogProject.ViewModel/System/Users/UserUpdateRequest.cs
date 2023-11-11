using BlogProject.Data.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogProject.ViewModel.System.Users
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Display(Name = "Thêm ảnh đại diện")]
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string? ImageFileName { get; set; }
        [Display(Name = "Giới tính")]
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
    }
}
  