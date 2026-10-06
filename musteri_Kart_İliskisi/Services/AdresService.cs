using musteri_Kart_İliskisi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Services
{
    public class AdresService
    {
        public void AdresEkle(Adres yeniAdres)
        {
            using (var db = new Context())
            {
                var musteriVarMi = db.Musteri.Any(m => m.MusteriID == yeniAdres.MusteriID);
                if (!musteriVarMi)
                {
                    throw new Exception("Müşteri bulunamadı");
                }

                db.Adres.Add(yeniAdres);
                db.SaveChanges();
            }
            ;
        }
        public List<Adres_Tipi> AdresTipleriniGetir()
        {
            using (var db = new Context())
            {
               return db.Adres_Tipi.ToList();
            }
        }
    }
}