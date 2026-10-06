using System;
using System.Collections.Generic;

namespace musteri_Kart_İliskisi.Entities.DTOS
{
    public class MusteriGuncelleDto
    {
        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Tc_No { get; set; }
        public string SigortaDurumu { get; set; }
        public string AnneKızlıkSoyadı { get; set; }
        public string Cinsiyet { get; set; }
        public string MusteriDurumu { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        // React'ten gelen listeleri karşılayacak özellikler
        public List<EmailGuncelleDto> Emailler { get; set; }
        public List<TelefonGuncelleDto> Telefonlar { get; set; }
        public List<AdresGuncelleDto> Adresler { get; set; }
    }

    // Alt tabloların (listelerin) içindeki nesneleri karşılayacak alt DTO'lar
    public class EmailGuncelleDto
    {
        public string EmailAdresi { get; set; }
    }

    public class TelefonGuncelleDto
    {
        public string Telefon_No { get; set; }
        public string telefon_Tipi { get; set; }
    }

    public class AdresGuncelleDto
    {
        public string Sehir { get; set; }
        public string İlce { get; set; }
        public string AdresDetay { get; set; }
        public string AdresTipi { get; set; }
    }
}