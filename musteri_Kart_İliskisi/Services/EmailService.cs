using musteri_Kart_İliskisi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Services
{
    public class EmailService
    {
        public void EmailEkle(E_mail yeniEmail)
        {
            using (var db = new Context())
            {
                var musteriVarMi = db.Musteri.Any(m => m.MusteriID == yeniEmail.MusteriId);
                if (!musteriVarMi)
                {
                    throw new Exception("Müşteri bulunamadı");
                }

                db.E_Mail.Add(yeniEmail);
                db.SaveChanges();
            }
        }
    }
}
