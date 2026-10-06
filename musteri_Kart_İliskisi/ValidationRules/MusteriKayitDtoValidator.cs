using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using musteri_Kart_İliskisi.Entities.DTOS;

namespace musteri_Kart_İliskisi.Services.ValidationRules
{
    public class MusteriKayitDtoValidator : AbstractValidator<MusteriKayitDto>
    {
        public MusteriKayitDtoValidator()
        {
            RuleFor(x => x.MusteriAdi)
                .NotEmpty().WithMessage("Müşteri adı boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Müşteri adı en az 2 karakter olmalıdır.");

            RuleFor(x => x.MusteriSoyadi)
                 .NotEmpty().WithMessage("Müşteri soyadı boş bırakılamaz.");

            RuleFor(x => x.Tc_No)
                .NotEmpty().WithMessage("TC No boş bırakılamaz")
                .Length(11).WithMessage("TC No 11 haneli olmalıdır")
                .Matches("^[0-9]*$").WithMessage("TC Kimlik numarası" +
                " sadece rakamlardan oluşmalıdır.");

            RuleFor(x => x.DogumTarihi)
            .NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz.")
            .GreaterThan(new DateTime(1900, 1, 1))
            .WithMessage("Lütfen 1900 yılından sonraki bir tarih girin.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Doğum tarihi bugünden ileri bir tarih olamaz.");

            RuleFor(x => x.AnneKizlikSoyadi)
                .NotEmpty().WithMessage("Anne kızlık soyadı boş geçilemez");

            // Doğum Yılı Kuralları
            RuleFor(x => x.DogumTarihi)
             .NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz.")
             .GreaterThan(new DateTime(1900, 1, 1)) // yıl - gün - ay şeklinde
             .WithMessage("Lütfen 1900 yılından sonraki bir tarih girin.")
             .LessThanOrEqualTo(DateTime.Now) // Bugünden ilerisi girilmesin diye
             .WithMessage("Doğum tarihi bugünden ileri bir tarih olamaz.");
             

            RuleFor(x => x.SigortaDurumu)
                .Must(durum => durum == "4A" || durum == "4B" || durum == "4C")
                .WithMessage("Sigorta durumu sadece '4A', '4B', '4C' olabilir.")
                .When(x => !string.IsNullOrEmpty(x.SigortaDurumu));

           /* RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli formatta email adresi giriniz."); */

            RuleFor(x => x.Cinsiyet)
                .Must(durum => durum == "E" || durum == "K")
                .WithMessage("Cinsiyet sadece 'E' ve 'K' olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Cinsiyet));

            RuleFor(x => x.MusteriDurumu)
                .Must(durum => durum == "A" || durum == "P")
                .WithMessage("Müşteri durumu Aktif(A) ya da Pasif(P) olabilir.")
                .When(x => !string.IsNullOrEmpty (x.MusteriDurumu));

        }
    }
}
