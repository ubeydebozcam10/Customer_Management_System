using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities
{
    public class E_mail
    {
        public int E_MailId { get; set; }
        public string EmailAdresi { get; set; }

        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; }
    }
}
