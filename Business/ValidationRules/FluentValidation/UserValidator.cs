using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
     public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.UserName.Length).GreaterThan(5).WithMessage("Kullanıcı adınız 5 karakterden uzun olmalıdır");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Geçerli bir Email Adesi giriniz");
            
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Kulllanıcı adı boş geçilmez...");
            RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("Doğum tarihi alanı boş geçilemez");
        
            
           


        }
    }
}
