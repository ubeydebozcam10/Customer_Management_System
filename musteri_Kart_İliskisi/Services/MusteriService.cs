using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using musteri_Kart_İliskisi.Entities; // Müşteri ve context iletişimi için
using System.Data.Entity;
using musteri_Kart_İliskisi.Entities.DTOS;

namespace musteri_Kart_İliskisi.Services
{
    public class MusteriService
    {
        // Listeleme metodu
        public List<MusteriListeleDto> MusterileriGetir()
        {
            using (var db = new Context())
            {
                return db.Musteri
                    .Include(m => m.Adres)
                    .Include(m => m.Telefon)
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

                        Adresler = m.Adres.Select(a => new AdresListeleDto
                        {
                            Id = a.Id,
                            AdresTipiID = a.AdresTipiID,
                            Il = a.Sehir,
                            Ilce = a.Ilce,
                            AdresDetay = a.AdresDetay
                        }).ToList(),

                        Telefonlar = m.Telefon.Select(t => new TelefonListeleDto
                        {
                            TelefonID = t.TelefonID,
                            TelefonTipiID = t.TelefonTipiID,
                            Numara = t.Numara
                        }).ToList()
                    }).ToList();
            }
        }

       //  Id'ye göre tek müşteri getirir.
        public MusteriListeleDto MusteriGetirDetayli(int id)
        {
            using (var db = new Context())
            {
                return db.Musteri
                    .Include(m => m.Adres)
                    .Include(m => m.Telefon)
                    .Where(m => m.MusteriID == id) 
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

                        Adresler = m.Adres.Select(a => new AdresListeleDto
                        {
                            Id = a.Id,
                            AdresTipiID = a.AdresTipiID,
                            Il = a.Sehir,
                            Ilce = a.Ilce,
                            AdresDetay = a.AdresDetay
                        }).ToList(),

                        Telefonlar = m.Telefon.Select(t => new TelefonListeleDto
                        {
                            TelefonID = t.TelefonID,
                            TelefonTipiID = t.TelefonTipiID,
                            Numara = t.Numara
                        }).ToList()

                    }).FirstOrDefault(); // Birden fazla müşteri dönmesini engeller, ilkini alır
            }
        }

        // Musteri eklemek icin
        public void MusteriEkle(Musteri YeniMusteri)
        {
            
            if (string.IsNullOrEmpty(YeniMusteri.Tc_No) || YeniMusteri.Tc_No.Length != 11)
            {
                throw new Exception("TC Kimlik numarası 11 haneli olmak zorundadır!");
            }

            using (var db = new Context())
            {
                db.Musteri.Add(YeniMusteri);
                db.SaveChanges();
            }
        }

        // Müşteri güncellemek için 
        // Müşteri güncellemek için 
        public void MusteriGuncelle(MusteriGuncelleDto dto)
        {
            using (var db = new Context())
            {
                // KİLİT NOKTA 1: Müşteriyi bulurken eski Adres, Telefon ve E-maillerini de (.Include ile) getiriyoruz ki silebilelim.
                var mevcutMusteri = db.Musteri
                    .Include(m => m.Adres)
                    .Include(m => m.Telefon)
                    .Include(m => m.E_mail)
                    .FirstOrDefault(m => m.MusteriID == dto.MusteriID);

                if (mevcutMusteri != null)
                {
                    // --- 1. TEMEL BİLGİLERİ GÜNCELLEME ---
                    if (!string.IsNullOrEmpty(dto.MusteriAdi)) mevcutMusteri.MusteriAdi = dto.MusteriAdi;
                    if (!string.IsNullOrEmpty(dto.MusteriSoyadi)) mevcutMusteri.MusteriSoyadi = dto.MusteriSoyadi;
                    if (dto.DogumTarihi.HasValue) mevcutMusteri.DogumTarihi = dto.DogumTarihi.Value;
                    if (!string.IsNullOrEmpty(dto.Tc_No)) mevcutMusteri.Tc_No = dto.Tc_No;
                    if (!string.IsNullOrEmpty(dto.AnneKızlıkSoyadı)) mevcutMusteri.AnneKızlıkSoyadı = dto.AnneKızlıkSoyadı;
                    if (!string.IsNullOrEmpty(dto.SigortaDurumu)) mevcutMusteri.SigortaDurumu = dto.SigortaDurumu;
                    if (!string.IsNullOrEmpty(dto.Cinsiyet)) mevcutMusteri.Cinsiyet = dto.Cinsiyet;
                    if (!string.IsNullOrEmpty(dto.MusteriDurumu)) mevcutMusteri.MusteriDurumu = dto.MusteriDurumu;

                    mevcutMusteri.GuncellemeTarihi = DateTime.Now;

                    // --- 2. E-POSTA GÜNCELLEME (Eskileri sil, yenileri ekle) ---
                    if (dto.Emailler != null && dto.Emailler.Count > 0)
                    {
                        db.E_Mail.RemoveRange(mevcutMusteri.E_mail); // Eski mailleri veritabanından sil
                        foreach (var e in dto.Emailler)
                        {
                            db.E_Mail.Add(new E_mail
                            {
                                EmailAdresi = e.EmailAdresi,
                                MusteriId = mevcutMusteri.MusteriID
                            });
                        }
                    }

                    // --- 3. TELEFON GÜNCELLEME ---
                    if (dto.Telefonlar != null && dto.Telefonlar.Count > 0)
                    {
                        db.telefon_No.RemoveRange(mevcutMusteri.Telefon); // Eski telefonları sil
                        foreach (var t in dto.Telefonlar)
                        {
                            db.telefon_No.Add(new telefon_No
                            {
                                Numara = t.Telefon_No,
                                TelefonTipiID = t.telefon_Tipi?.ToUpper(),
                                MusteriId = mevcutMusteri.MusteriID
                            });
                        }
                    }

                    // --- 4. ADRES GÜNCELLEME ---
                    if (dto.Adresler != null && dto.Adresler.Count > 0)
                    {
                        db.Adres.RemoveRange(mevcutMusteri.Adres); // Eski adresleri sil
                        foreach (var a in dto.Adresler)
                        {
                            db.Adres.Add(new Adres
                            {
                                Sehir = a.Sehir,
                                Ilce = a.İlce,
                                AdresDetay = a.AdresDetay,
                                AdresTipiID = a.AdresTipi,
                                MusteriID = mevcutMusteri.MusteriID
                            });
                        }
                    }

                    // Tüm bu değişiklikleri veritabanına kaydet
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Güncellenmek istenen müşteri bulunamadı!");
                }
            }
        }

        // Müşteri silmek icin
        public void MusteriSil(int MusteriID)
        {
            using (var db = new Context())
            {
                var silinecekMusteri = db.Musteri.Find(MusteriID);

                if (silinecekMusteri != null)
                {
                    // Müşteriyi veritabanından kaldırıyoruz
                    db.Musteri.Remove(silinecekMusteri);
                    db.SaveChanges();
                }
                else
                {
                    // Eğer o ID'ye sahip bir müşteri yoksa hata fırlatıyoruz
                    throw new Exception("Silinmek istenen müşteri veritabanında bulunamadı!");
                }
            }
        }
    }
}