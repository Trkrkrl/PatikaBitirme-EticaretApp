using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }//satınalan kişi
        public int SellerId { get; set; }//ürünü ekleyen kişinin bilgisi  için
        public DateTime OrderDate { get; set; }
        public int AdressId { get; set; }
        public int TotalAmount { get; set; }//ürün satınalma fiyatı
    }
}
