using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace musteri_Kart_İliskisi.Entities.DTOS
{
    public class MusteriListeleDto
    {
        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Tc_No { get; set; }
        public string SigortaDurumu { get; set; }
        public string AnneKızlıkSoyadı { get; set; }
        public string Cinsiyet { get; set; }
        public string MusteriDurumu { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        public List<AdresListeleDto> Adresler { get; set; }
        public List<TelefonListeleDto> Telefonlar { get; set; }

        // Müşterinin birden fazla e-postası olabileceği için Liste olarak tutuyoruz
        public List<EmailListeleDto> Email { get; set; }
    }

    // Adres için alt DTO
    public class AdresListeleDto
    {
        public int Id { get; set; }
        public string AdresTipiID { get; set; } // 'E' veya 'I' dönecek
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string AdresDetay { get; set; }
    }

    // Telefon için alt DTO
    public class TelefonListeleDto
    {
        public int TelefonID { get; set; }
        public string TelefonTipiID { get; set; } // 'H', 'W' veya 'M' dönecek
        public string Numara { get; set; }
    }

    // Email için alt DTO (DÜZELTİLEN KISIM BURASI)
    public class EmailListeleDto
    {
        public string EmailAdresi { get; set; }
    }
}