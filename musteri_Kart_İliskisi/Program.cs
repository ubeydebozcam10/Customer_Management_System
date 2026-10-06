using musteri_Kart_İliskisi.Entities;
using musteri_Kart_İliskisi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi
{
    public class Program
    {
     public  static void Main(string[] args)
        {
            MusteriService service = new MusteriService();

            Musteri yeniMusteri = new Musteri
            {
                MusteriAdi = "Ali",
                MusteriSoyadi = "Veli",
               // DogumTarihi = "1970",
                Tc_No = "12345678901",
                AnneKızlıkSoyadı = "Kaya",
                SigortaDurumu = "SGK"
            };
           // service.MusteriEkle(yeniMusteri);

            Musteri yeniMusteri1 = new Musteri
            {
                MusteriAdi = "Ayşe",
                MusteriSoyadi = "Dağ",
                // DogumYili = "1990",
                Tc_No = "32165498731",
                AnneKızlıkSoyadı = "Dere",
                SigortaDurumu = "Emekli Sandığı"  
            };
            //  service.MusteriEkle(yeniMusteri1);


            // service.MusteriSil(4);
            // service.MusteriSil(5);
            Console.ReadLine();
        }
    }
}
