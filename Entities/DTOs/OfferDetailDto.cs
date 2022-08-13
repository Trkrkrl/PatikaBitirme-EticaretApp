using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OfferDetailDto:IDto
    {
        public int OfferId { get; set; }
        public double OfferPercentage { get; set; }
        public int OfferAmount { get; set; }
        public int ProductId { get; set; }
        public string offerStatus { get; set; }//bu productun statusu buna elleşme
        //--
        public int SenderUserId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        //-
        public int ReceiverUserId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverLastName { get; set; }
        //- 
       

    }
}
