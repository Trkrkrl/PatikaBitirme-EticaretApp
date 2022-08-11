using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Offer : IEntity
    {
        public int OfferId { get; set; }
        public double OfferPercentage { get; set; } = 100;
        public int OfferAmount { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; } = "Pending";//Teklif Kabul ve red durumuna göre


    }
}
