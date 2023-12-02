using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogProject.ViewModel.System.Users
{
    public class ChangePassword
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu không khớp.")]
        public string ComfirmPass { get; set; }
        public string CurrentPassword { get; set; }
    }
}
