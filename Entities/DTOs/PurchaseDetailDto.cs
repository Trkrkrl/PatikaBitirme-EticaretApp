using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PurchaseDetailDto
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }//satınalan kişi
        public int SellerId { get; set; }//ürünü ekleyen kişinin bilgisi  için
        public DateTime OrderDate { get; set; }
        public int AdressId { get; set; }
        public int TotalAmount { get; set; }//ürün satınalma fiyatı
        //--
        public string  BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public Address DeliveryAdress { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
