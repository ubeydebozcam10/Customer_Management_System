using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using musteri_Kart_İliskisi.Entities;


namespace musteri_Kart_İliskisi.Services
{
    public class TelefonService
    {
        public void TelefonEkle(telefon_No yeniTelefon)
        {
            using (var db = new Context())
            {
                var musteriVarMi = db.Musteri.Any(m => m.MusteriID == yeniTelefon.MusteriId);
                if (!musteriVarMi)
                {
                    throw new Exception("müşteri bulunamadı");
                }

                db.telefon_No.Add(yeniTelefon);
                db.SaveChanges();
            }
        }

        public List<telefon_Tipi> TelefonTipleriniGetir()
        {
            using (var db = new Context())
            {
                return db.telefon_Tipi.ToList();
            }
        }
    }
}

