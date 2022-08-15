using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public static string UserRegistered = "Kullanıcı kaydedildi";
        public static string AccessTokenCreated = "AccessToken Oluşturuldu";

        public static string SuccessfulLogin = "Login Başarılı";
        public static string PasswordError = "Parola Hatası";
        public static string UserNotFound = "Kullanıcı Bulunamadı";

        public static string UserDeleted = "Kullanıcı Silindi";

        public static string CategoriesListed = "Kategoriler Listelendi";
        public static string CategoryAdded = "Kategori Eklendi ";
        public static string CategoryDeleted = "Kategori Silindi ";

        public static string ColorsListed = "Renkler Listelendi ";

        public static string ColorDeleted = "Renk Silindi ";
        public static string ColorAdded = "Renk Eklendi ";

        public static string ProductAdded = "Ürün Eklendi ";

        public static string ProductDeleted = "Ürün Silindi ";

        public static string ProductUpdated = "Ürün Güncellendi ";

        public static string ProductsListed = "Ürünler Listelendi ";

        public static string CategoryDoesntExist = "Böyle Bir Kategori Bulunamadı ";
        public static string ColorDoesntExist = "Böyle Bir Renk Bulunamadı ";

        public static string OfferSent = "Teklifiniz Gönderildi ";
        public static string OfferDeleted = "TeklifiniziBaşarıyla Geri Çektiniz/Sildiniz  ";

        public static string OfferUpdated = "Teklifiniz Başarıyla Güncellendi";

        public static string ProductIsNotOfferable = "Bu ürüne teklif yapılamaz";

        public static string OfferIsPending = "Bu ürüne yapılmış ve aktif olan bir teklifiniz mevcut";

        public static string OfferIsAccepted2 = "Bu ürüne yapmış olduğunuz teklif kabul edilmiş. Siparişlerim sayfasından iptal edebilirsiniz";

        public static string OfferIsAccepted3 = "Bu ürüne yapmış olduğunuz teklif kabul edilmiş. Siparişlerim sayfasından iptal edebilirsiniz";

        public static string PurchaseAdded = "Siparişiniz eklendi";

        public static string PurchaseCanceled = "Siparişiniz silindi/iptal edildi";

        public static string PurchasesListed = "Tüm siparişler Listelendi";

        public static string PurchasesListed2 = "Tüm siparişleriniz Listelendi";

        public static string PurchasesListed3 = "Sipariş detayları getirildi";

        public static string AddressAdded = "Adresiniz başarıyla eklendi";

        public static string AddressDeleted = "Adresiniz başarıyla silindi";

        public static string AddressUpdated = "Adresiniz başarıyla güncellendi";

        public static string PurchaseFromOfferAdded = "Tekliften yönlendirilen siparişiniz başşarıyla eklendi ";

        public static string OfferAccepted = "Teklif başarıyla kabul edildi";

        public static string OfferAcceptedAndReliedToPurchase = "Teklifiniz tam fiyat üzerinden olduğundan  kabul edildi ve satınalmaya yönlendirildi";

        public static string ImageSizeIsHigh = "Görsel boyutu 400kb 'den düşük olmalıdır.";

        public static string UserNameExists = "Bu kullanıcı adı kullanılmaktadır.";

        public static string EmailOnUse = "Bu email kullanılmaktadır.";

        public static string UserSuspended = "Bu Kullanıcının hesabı askıya alınmıştır .";
    }
}
