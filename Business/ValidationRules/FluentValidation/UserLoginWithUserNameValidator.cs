using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserLoginWithUserNameValidator : AbstractValidator<UserNameLoginDto>
    {
        public UserLoginWithUserNameValidator()
        {
            // boş bırakılmamalı
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Kullanıcı adı kısmı boş  bırakılamaz!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre kısmı boş  bırakılamaz!");
            

            

        }
        
    }
}
