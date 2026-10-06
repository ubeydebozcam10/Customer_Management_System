using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities.DTOS
{
    public class AdresDto
    {
        public string AdresDetay{ get; set; }
        public string AdresTipi { get; set; }
        public string Sehir {  get; set; }
        public string İlce { get; set; }

    }

    public class TelefonDto
    {
        public string Telefon_No { get; set; }
        public string telefon_Tipi { get; set; }
    }

    public class EmailDto
    {
        public string EmailAdresi { get; set; }
    }

    public class MusteriKayitDto
    {
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public string Tc_No { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string SigortaDurumu { get; set; }
        public string AnneKizlikSoyadi { get; set; }
        public string Cinsiyet {  get; set; }
        public string MusteriDurumu { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        public List<AdresDto> Adresler { get; set; } = new List<AdresDto>();
        public List<TelefonDto> Telefonlar { get; set; } = new List<TelefonDto>();
        public List<EmailDto> Emailler { get; set; } = new List<EmailDto>();
    }
}