using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using musteri_Kart_İliskisi.Entities.DTOS;

namespace musteri_Kart_İliskisi.Services.ValidationRules
{
    public class MusteriGuncelleDtoValidator : AbstractValidator<MusteriGuncelleDto>
    {
        public MusteriGuncelleDtoValidator()
        {
            RuleFor(x => x.MusteriID)
                .GreaterThan(0).WithMessage("Geçerli bir Müşteri ID girmelisiniz.");

            // Ad gönderilmişse en az 2 karakter olmalı (When = Eğer boş değilse bu kuralı çalıştır)
            RuleFor(x => x.MusteriAdi)
                .MinimumLength(2).WithMessage("Müşteri adı en az 2 karakter olmalıdır.")
                .When(x => !string.IsNullOrEmpty(x.MusteriAdi));

            // TC Kimlik gönderilmişse 11 hane ve rakam olmalı
            RuleFor(x => x.Tc_No) // boş olamaz diye de kural ekle
                .NotEmpty().WithMessage("TC Kimlik numarası boş bırakılamaz")
                .Length(11).WithMessage("TC Kimlik numarası tam olarak 11 haneli olmalıdır")
                .Matches("^[0-9]*$").WithMessage("TC Kimlik numarası sadece rakamlardan oluşmalıdır.")
                .When(x => !string.IsNullOrEmpty(x.Tc_No));

            // Doğum Yılı Kuralları (Sadece dolu gönderilirse çalışır)
            RuleFor(x => x.DogumTarihi) // 2002 - 03 - 18 
            .NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz.")
            .GreaterThan(new DateTime(1900, 1, 1)) // yıl - gün - ay şeklinde 
            .WithMessage("Lütfen 1900 yılından sonraki bir tarih girin.")
            .LessThanOrEqualTo(DateTime.Now) // Bugünden ilerisi girilmesin diye
            .WithMessage("Doğum tarihi bugünden ileri bir tarih olamaz.")
            .When(x => x.DogumTarihi.HasValue );

            RuleFor(x => x.SigortaDurumu)
                .Must(durum => durum == "4A" || durum == "4B" || durum == "4C")
                .WithMessage("Sigorta durumu sadece '4A', '4B', '4C' olabilir.")
                .When(x => !string.IsNullOrEmpty(x.SigortaDurumu));

            // listeler için RuleFor yerine RuleForEach kullanılır.
            // listeler için RuleForEach kullanılır, ancak liste içinde nesne olduğu için ChildRules ile nesnenin içine giriyoruz.
            RuleForEach(x => x.Emailler)
                .ChildRules(emailNesnesi =>
                {
                    emailNesnesi.RuleFor(e => e.EmailAdresi)
                                .EmailAddress().WithMessage("Email adreslerini düzgün formatta giriniz.");
                })
                .When(x => x.Emailler != null && x.Emailler.Count > 0);

            RuleFor(x => x.Cinsiyet)
              .Must(durum => durum == "E" || durum == "K")
              .WithMessage("Cinsiyet sadece 'E' ve 'K' olabilir.")
              .When(x => !string.IsNullOrEmpty(x.Cinsiyet));

            RuleFor(x => x.MusteriDurumu)
               .Must(durum => durum == "A" || durum == "P")
               .WithMessage("Müşteri durumu Aktif(A) ya da Pasif(P) olabilir.")
               .When(x => !string.IsNullOrEmpty(x.MusteriDurumu));
        }
    }
} 