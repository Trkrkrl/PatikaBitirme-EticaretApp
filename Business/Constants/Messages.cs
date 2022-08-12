using Core.Entities.Concrete;
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
    }
}
