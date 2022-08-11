using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OfferManager:IOfferService
    {/* ürün bilgisi is offerabele ise teklif yapılabilmeli-
      * bunun için teklif yap içerisine if else ile veya altta bir method ile offerable kontrolü
      * 
      * Teklif yap a basıldığında buradan add offer çalışacak
      * fakat bu offerdaki yüzdeyi işleyecceğiz
      * eğer 100 ise offerstatus :accepted olsun=direkt purchase managere gitsin 
      * değilse teklif süreçleri işlesin
      * 
      * kullanıcı teklif iptal edebilsin
      * 
      * 2 tip get offers olmalı
      * biri user ın customerinin yaptıklarını
      * diğeri userin sellerinin yaptıkalrı
      * 
      * 
      * 
      * 
      * Bir de satıcı tarafında offer kabul etme durumu olacak
      * kabul ederse söz konusu offer tamamlanır- => durumu accpeted olur=> purchase managere gitsin - bu ve yukardaki 100 durumlarını bağlayabilrim
      * 
      * 
      */
    }
}
