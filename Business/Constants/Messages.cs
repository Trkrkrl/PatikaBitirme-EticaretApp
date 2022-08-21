using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        public static string CouldNotCreateUser = "Kullanıcı oluşturulamadı çünkü E-Posta veya Kulllanıcı Adı Başkası tarafından kullanılmakta .";

        public static string PasswordUpdated = "Şifreniz değiştirldi .";

        public static string OldPasswordIsWrong = "Mevcut şifrenizi yanlış girdiniz.";

        public static string UnAuthorizedDeleteAttempt = "Bu  hesabı silmeye yetkiniz yok";

        public static string UnAthorizedAttempt2 = "Bu  ürüne görsel ekleme yetkiniz  yok ";

        public static string UnAthorizedProductUpdateAttempt = "Bu  ürünü güncelleme yetkiniz yok ";
        public static string UnAthorizedProductDeleteAttempt = "Bu  ürünü silmeye yetkiniz yok ";

        public static string UnAthorizedProductImageDeleteAttempt = "Bu  ürün görselini silmeye yetkiniz yok ";
        public static string UnAthorizedProductImageUpdateAttempt = "Bu  ürün görselini güncelleme yetkiniz yok ";

        public static string YouAreNotAllowedToViewThisOffer = "Bu  teklifi görüntüleme yetkiniz yok ";

        public static string YouAreNotAllowedToViewThisOfferDetails = "Bu  teklifin detaylarını görüntüleme yetkiniz yok ";

        public static string NotAllowedToAcceptThisOffer = "Bu  teklifi kabul etmeye yetkiniz yok ";

        public static string OfferDeclinedBySeller = "Bu  teklif satıcı tarafından reddedildi ";

        public static string OtherOffersDeclinedAutomatically = "Diğer müşterilerin teklifleri otomatik olarak reddedildi ";

        public static string AuthorizationDenied = "Buna yetkiniz yok ";

        public static string ColorUpdated = "Renk Güncellendi";

        public static string ConsumerStarted = "Kuyruk tüketmeye başladı";
        public static string UserSuspended2 = " Değerli kullanıcı. Şifrenizi 3 kere hatalı girdiğinizden dolayı Hesabınız 10 dk süreyle askıya alınmıştır .";

        public static string ProductImageAdded = "Ürün görseli başarıyla  eklendi";

        public static string ProductImageUpdated = "Ürün görseli başarıyla  güncellendi";
        public static string ProductImageDeleted = "Ürün görseli başarıyla  silindi";

        public static string RegistrationFailed = "Kayıt oluşturma başarısız.";

        public static string ThisEmailIsNotAvailable = "Bu email adresi başka bir kullanıcıya aittir";

        public static string UserNotFountWithMail = "Bu  emaille bir hesap bulunamadı";

        public static string UserNotFountWithUserName = "Bu kullanıcı adıyla bir kullanıcı bulunamadı";

        public static string CouldNotFindUser = "Bu kullanıcı adına  kayıtlı kullanıcı bulunamadı";

        public static string CouldNotFindUser2 = "Bu emaile kayıtlı kulllanıcı bulunamadı";

        public static string BrandAdded = "Marka eklendi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandUpdated = "Marka güncellendi";
        public static string BrandsListed = "Markalar listelendi";

        public static string CategoryUpdated = "Kategori güncellendi";

        public static string YourOfferDeclined = "Teklifiniz  satıcı tarafından reddedilmiştir";
        public static string OfferAcceptedMail = "Teklifiniz  satıcı tarafından kabul edilmiştir";
    }
}
