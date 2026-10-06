using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities
{
    
    public class Musteri
    {
        public Musteri()
        {
            this.KayıtTarihi = DateTime.Now;
        }

        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public string  MusteriSoyadi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Tc_No { get; set; }
        public string SigortaDurumu { get; set; }
        public string AnneKızlıkSoyadı { get; set; }
        public string Cinsiyet {  get; set; }
        public string MusteriDurumu { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        public ICollection<E_mail> E_mail { get; set; }
        public ICollection <telefon_No> Telefon { get; set; }
        public ICollection <Adres> Adres { get; set; }
    }
}
