using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<ProductDetailDto>
    {
        public ProductValidator()
        {
            //Boş bırakılamaz
            RuleFor(p=>p.ProductName).NotEmpty().WithMessage("Ürün adı  bırakılamaz!");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Ürün açıklaması kısmı boş  bırakılamaz!");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategori ksımı boş bırakılamaz!");
            RuleFor(p => p.Status).NotEmpty().WithMessage("Ürünün kullanım durumu boş bırakılamaz");
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Ürün Fiyatı  boş bırakılamaz");






            //karakter sınırlaması
            RuleFor(u => u.ProductName.Length).LessThanOrEqualTo(100).WithMessage("Ürün Adı kısmı 100 karakterden uzun olamaz!");
            RuleFor(u => u.Description.Length).LessThanOrEqualTo(100).WithMessage("Ürün Açıklaması kısmı 500 karakterden uzun olamaz!");

            RuleFor(p => p.Description.Length).GreaterThanOrEqualTo(10).WithMessage("Açıklama kısmı 10 karakterden kısa olamaz!");

            RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Ürün Fiyatı 0'dan büyük olmalıdır");

           



        }
    }
}
