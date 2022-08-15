using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class OfferValidator : AbstractValidator<Offer>
    {
        public OfferValidator()
        {
            //offer'ın yüzde ve  amount kısımları sayı olmalı-bunu  yapamadım
            //offerda ya yüzde yada miktar kısımlları girili olmalı 2si aynı anda olmaz
            
            RuleFor(o => o.OfferPercentage)
                .GreaterThan(0).When(o => o.OfferAmount == 0)
                .Equal(0).When(o => o.OfferAmount != 0);

            //yüzde kısmı 100 den büyük olamaz
            RuleFor(o => o.OfferPercentage).LessThanOrEqualTo(100).WithMessage("Teklif yüzdesi 100'den yüksek olamaz");

            //Bir göndereni olmalı-ki bu kayıtlı olduğunu gösterir
            RuleFor(o => o.SenderUserId)
                .NotEmpty().WithMessage("Teklif yapanın UserId'si boş veya 0 olamaz")
                .GreaterThan(0);
            
                

        }
    }
}
