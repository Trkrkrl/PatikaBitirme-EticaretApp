    using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation

{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a=>a.AdressName).NotEmpty().WithMessage("Adresinize bir ad vermelisiniz");    
            RuleFor(a=>a.City).NotEmpty().WithMessage("İl Kısmı Boş Bırakılamaz");
            RuleFor(a => a.County).NotEmpty().WithMessage("İlçe Kısmı Boş Bırakılamaz");
            RuleFor(a => a.BuildingNo).NotEmpty().WithMessage("Bina Numarası Boş Bırakılamaz");


        }
    }
}
