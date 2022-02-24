using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public class Messages
    {
        public static string SuccessAdded = "Listeye Başarıyla Eklendi.";
        public static string ErrorAdd = "Veri Eklenemedi.";
        public static string SuccessDeleted = "Listeden Başarıyla Silindi.";
        public static string SuccessUpdated = "Veri Başarıyla Güncellendi.";
        public static string ItemNameInValid = "Veri ismi en az 2 karakter olmalı.";
        public static string MaintenanceTime = "Sistem Bakımda. Lütfen sonra deneyiniz.";
        public static string ItemsListed = "Veriler Başarıyla Listelendi.";
        public static string ItemExist = "Eklemek İstediğiniz Veri Zaten Bulunuyor.";
        public static string DataNotFound = "Veri Bulunamadı.";
        public static string CarNotOnRent = "Araç Şu Anda Kiralanamıyor. Lütfen Başka Araç Deneyiniz.";
        public static string CarImageCountExceed = "Arabanın Azami Resim Sayısı Aşıldı. 1 Arabanın En Fazla 5 Resmi Olabilir.";
        public static string ImageAdded = "Resim Başarıyla Yüklendi.";
        public static string UserAlreadyExist = "Bu Kullanıcı Zaten Mevcut.";
        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Geçersiz Şifre.";
        public static string SuccessfulLogin = "Giriş Başarılı.";
        public static string SuccessfulRegister = "Kayıt Başarılı.";
        public static string AccessTokenCreated = "Access Token Oluşturuldu.";
    }
}
