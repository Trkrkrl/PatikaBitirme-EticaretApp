using Core.Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
     public class UserLoginWithMailValidator : AbstractValidator<UserMailLoginDto>
    {
        public UserLoginWithMailValidator()
        {
            // boş bırakılmamalı
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email adresiniz kısmı boş  bırakılamaz!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre kısmı boş  bırakılamaz!");
            //

            RuleFor(c => c.Email).EmailAddress().WithMessage("Geçerli bir Email Adresi giriniz");
            
            
           


        }
    }
}
