using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Purchase : IEntity
    {
        [Key]
        public int PurchaseId { get; set; }
        public int UserId { get; set; }//satınalan kişi
        public int SellerId { get; set; }//ürünü ekleyen kişinin bilgisi  için
        public DateTime OrderDate { get; set; }
        public int AdressId { get; set; }
        public int TotalAmount { get; set; }//ürün satınalma fiyatı
        public int ProductId { get; set; }
    }
}
