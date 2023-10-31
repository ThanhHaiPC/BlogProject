using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.System.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên tài khoản không được để trống").OverridePropertyName("UserName");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống").OverridePropertyName("Password");
        }
    }
}
