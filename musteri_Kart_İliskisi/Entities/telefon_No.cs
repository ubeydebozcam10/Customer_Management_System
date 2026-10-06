using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities
{
    public class telefon_No
    {
        public int TelefonID { get; set; }
        public string Numara { get; set; } 

        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; }

        public string TelefonTipiID { get; set; }
        public telefon_Tipi TelefonTipi { get; set; }
    }
}
