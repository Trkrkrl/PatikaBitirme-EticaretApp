using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserResigterValidator: AbstractValidator<UserForRegisterDto>
    {
       
        public UserResigterValidator()
            {// not empty
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Kulllanıcı adı boş bırakılamaz!");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Adınız kısmı boş bırakılamaz!");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Soyadınız kısmı boş bırakılamaz!");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email adresiniz kısmı boş  bırakılamaz!");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Telefon numaranız  kısmı boş  bırakılamaz!");
            RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("Doğum tarihi kısmı boş  bırakılamaz!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre kısmı boş  bırakılamaz!");
            RuleFor(u => u.passwordAgain).NotEmpty().WithMessage("Şifre tekrar kısmı boş  bırakılamaz!");
            // karakter sınırlaması
            RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(8).WithMessage("Şifreniz 8 karakterden kısa olamaz!");
            RuleFor(u => u.Password.Length).LessThanOrEqualTo(20).WithMessage("Şifreniz 20 karakterden uzun olamaz!");
            // parola 1 büyük harf ve 1 sayı
            RuleFor(u => u.Password)
                .Matches("[A-Z]").WithMessage("Şifreniz en az 1 adet büyük karakter içermelidir!")
                .Matches("[a-z]").WithMessage("Şifreniz en az 1 adet küçük karakter içermelidir!")
                .Matches(@"\d").WithMessage("Şifreniz en az 1 adet rakam içermelidir!")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Şifreniz en az 1 adet noktalama işareti  içermelidir!.");
            // parolalar aynı
            RuleFor(u => u.passwordAgain).Equal(u => u.Password).WithMessage("Girmiş olduğunuz şifreler aynı olmalıdır");
            //doğum tarihi geçerli
            RuleFor(u => u.DateOfBirth).LessThanOrEqualTo(DateTime.Now.Date.AddDays(-18)).WithMessage(" Kayıt olmak için 18 yaşını doldurmuş olmanız gerekmektedir!");
            //email geçerli olmalı
            
            RuleFor(c => c.Email).EmailAddress().WithMessage("Geçerli bir Email Adresi giriniz");
             RuleFor(u=>u.PhoneNumber)
                .MinimumLength(10).WithMessage("Telefon numaranız en az 10 rakamdan oluşmalıdır.")
                .MaximumLength(16).WithMessage("Telefon numaranız 16 karakterden uzun olamaz.")
                .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Telefon nummaranız geçerli formatta değildir. Uygun Format şu şekildedir: +90 555 123 4567");
        

        ///








        RuleFor(c => c.UserName.Length).GreaterThan(5).WithMessage("Kullanıcı adınız 5 karakterden uzun olmalıdır");

                





            }
        
    }
}
