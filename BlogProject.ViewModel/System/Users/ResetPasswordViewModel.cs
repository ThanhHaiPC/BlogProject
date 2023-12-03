using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.System.Users
{
    public class ResetPasswordViewModel
    {
      
        public string Token { get; set; }

      
        public string Email { get; set; }


        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\W).*$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ hoa và một ký tự đặc biệt.")]
        public string NewPassword { get; set; }


        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu không khớp.")]
        public string ComfirmPass { get; set; }
    }
}
