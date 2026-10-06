using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities
{
    public class Adres
    {
        public int Id { get; set; }

        public string AdresDetay { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }

        public int MusteriID { get; set; }
        public virtual Musteri Musteri { get; set; }

        public string AdresTipiID { get; set; }

        public Adres_Tipi AdresTipi { get; set; }
    }
}
