using musteri_Kart_İliskisi.Entities;
using musteri_Kart_İliskisi.Entities.DTOS;
using musteri_Kart_İliskisi.Services;
using System;
using System.Data.Entity; 
using System.Linq;
using System.Web.Http;
using System.Data.Entity.Validation;

namespace musteri_Kart_İliskisi.API.Controllers
{
    public class MusteriController : ApiController
    {
        private MusteriService _musteriService = new MusteriService();

        // Müşteri ekleme metodu
        [HttpPost]
        public IHttpActionResult MusteriEkle([FromBody] MusteriKayitDto dto)
        {
            try
            {
                var validator = new musteri_Kart_İliskisi.Services.ValidationRules.MusteriKayitDtoValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    string hataMesajlari = string.Join("", validationResult.Errors.Select(e => e.ErrorMessage));
                    return BadRequest("Kayıt Doğrulama Hataları:" + hataMesajlari);
                }

                var yeniMusteri = new Musteri
                {
                    MusteriAdi = dto.MusteriAdi,
                    MusteriSoyadi = dto.MusteriSoyadi,
                    DogumTarihi = dto.DogumTarihi,
                    Tc_No = dto.Tc_No,
                    SigortaDurumu = dto.SigortaDurumu,
                    AnneKızlıkSoyadı = dto.AnneKizlikSoyadi,
                    Cinsiyet = dto.Cinsiyet,
                    MusteriDurumu = dto.MusteriDurumu,
                    KayıtTarihi = dto.KayıtTarihi,
                    GuncellemeTarihi = dto.GuncellemeTarihi,

                    Adres = dto.Adresler?.Select(a => new Adres
                    {
                        AdresDetay = a.AdresDetay,
                        Sehir = a.Sehir,
                        Ilce = a.İlce,
                        AdresTipiID = a.AdresTipi
                    }).ToList(),

                    Telefon = dto.Telefonlar?.Select(t => new telefon_No
                    {
                        Numara = t.Telefon_No,
                        TelefonTipiID = t.telefon_Tipi?.ToUpper()
                    }).ToList(),

                    E_mail = dto.Emailler?.Select(e => new E_mail
                    {
                        EmailAdresi = e.EmailAdresi
                    }).ToList(),
                };

                _musteriService.MusteriEkle(yeniMusteri);
                return Ok("Müşteri ve bağlı tüm bilgileri başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                Exception gercekHata = ex;

                while (gercekHata.InnerException != null)
                {
                    gercekHata = gercekHata.InnerException;
                }

                return BadRequest("GERÇEK HATA ŞU: " + gercekHata.Message);
            }
        }


        //  Id'ye göre tek müşteri getirme metodu
        [HttpGet]
        public IHttpActionResult GetMusteriById(int id)
        {
            try
            {
                // Servisten metodumuzu çağırıyoruz
                var musteri = _musteriService.MusteriGetirDetayli(id);

                // Eğer veritabanında öyle bir ID yoksa 404 döndür
                if (musteri == null)
                {
                    return NotFound();
                }

                // Bulunduysa 200 OK ile birlikte müşteriyi döndür
                return Ok(musteri);
            }
            catch (Exception ex)
            {
                return BadRequest("Getirme Hatası: " + ex.Message);
            }
        }

        // Müşteri güncelleme metodu
        [HttpPut]
        public IHttpActionResult MusteriGuncelle([FromBody] MusteriGuncelleDto dto)
        {
            try
            {

                var validator = new musteri_Kart_İliskisi.Services.ValidationRules.MusteriGuncelleDtoValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    string hataMesajlari = string.Join("", validationResult.Errors.Select(e => e.ErrorMessage));
                    return BadRequest("Güncelleme Kuralları İhlali:" + hataMesajlari);
                }

                if (dto.MusteriID <= 0)
                {
                    return BadRequest("Geçerli bir Müşteri ID'si girmelisiniz.");
                }

                _musteriService.MusteriGuncelle(dto);

                return Ok("Müşteri bilgileri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest("Güncelleme Hatası: " + ex.Message);
            }
        }

        // Müşteri silme metodu
        [HttpDelete]
        public IHttpActionResult MusteriSil(int id)
        {
            try
            {
                // Eğer ID 0 veya daha küçükse hatalı işlem
                if (id <= 0)
                {
                    return BadRequest("Geçerli bir Müşteri ID'si girmelisiniz.");
                }

                // Servisteki silme fonksiyonunu çağırıyoruz
                _musteriService.MusteriSil(id);

                return Ok("Müşteri başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("Silme Hatası: " + ex.Message);
            }
        }

        //  Müşteri listeleme metodu
        [HttpGet]
        public IHttpActionResult GetMusteriler()
        {
            try
            {
                using (var context = new Context())
                {
                    var musteriListesi = context.Musteri
                        .Include(m => m.Adres)
                        .Include(m => m.Telefon)
                        .Include(m => m.E_mail) // 1. EKLENEN KISIM: E-posta tablosunu da çek
                        .Select(m => new MusteriListeleDto
                        {
                            MusteriID = m.MusteriID,
                            MusteriAdi = m.MusteriAdi,
                            MusteriSoyadi = m.MusteriSoyadi,
                            DogumTarihi = m.DogumTarihi,
                            Tc_No = m.Tc_No,
                            SigortaDurumu = m.SigortaDurumu,
                            AnneKızlıkSoyadı = m.AnneKızlıkSoyadı,
                            Cinsiyet = m.Cinsiyet,
                            MusteriDurumu = m.MusteriDurumu,
                            KayıtTarihi = m.KayıtTarihi,
                            GuncellemeTarihi = m.GuncellemeTarihi,

                            // Adresleri alt DTO'ya dönüştürüyoruz
                            Adresler = m.Adres.Select(a => new AdresListeleDto
                            {
                                Id = a.Id,
                                AdresTipiID = a.AdresTipiID,
                                Il = a.Sehir,
                                Ilce = a.Ilce,
                                AdresDetay = a.AdresDetay
                            }).ToList(),

                            // Telefonları alt DTO'ya dönüştürüyoruz
                            Telefonlar = m.Telefon.Select(t => new TelefonListeleDto
                            {
                                TelefonID = t.TelefonID,
                                TelefonTipiID = t.TelefonTipiID,
                                Numara = t.Numara
                            }).ToList(),

                            // 2. EKLENEN KISIM: E-postaları alt DTO'ya veya string listesine dönüştürüyoruz
                            Email = m.E_mail.Select(e => new EmailListeleDto // Eğer DTO yazmadıysan sadece e.EmailAdresi diyerek List<string> de dönebilirsin
                            {
                                EmailAdresi = e.EmailAdresi
                            }).ToList()

                        }).ToList();

                    if (musteriListesi.Count == 0)
                    {
                        return NotFound();
                    }

                    return Ok(musteriListesi);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Listeleme Hatası: " + ex.Message);
            }
        }
    }
}
        