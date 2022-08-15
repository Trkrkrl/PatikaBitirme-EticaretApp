using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c=>c.CategoryName).NotEmpty().WithMessage("Kategori adı kısmı boş bırakılamaz");
            RuleFor(c => c.CategoryName.Length).GreaterThan(4).WithMessage("Kategori adı 4 karakterden büyük olmalıdır");



        }
    }
}
