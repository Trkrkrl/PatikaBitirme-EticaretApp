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
        public double OfferPercentage { get; set; } = 0;
        public int OfferAmount { get; set; } = 0;
        public int ProductId { get; set; }
        public string offerStatus { get; set; } = "Pending";//Teklif Kabul ve red durumuna göre
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }


    }
}
