﻿using Core.Entities.Concrete;
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
    }
}
