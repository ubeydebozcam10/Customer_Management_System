using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities.Mapping
{
    public class MusteriMap : EntityTypeConfiguration <Musteri>
    {
        public MusteriMap () {

            ToTable("Musteri");

            HasKey(m => m.MusteriID);

            Property(a => a.MusteriAdi).IsRequired();

            Property(a => a.MusteriSoyadi).IsRequired();

            Property(d => d.DogumTarihi)
                .IsRequired();


            Property(t => t.Tc_No)
               .IsRequired()
               .HasMaxLength(11)
               .IsFixedLength(); 

            Property(s => s.SigortaDurumu)
                .IsRequired();

            Property(v => v.AnneKızlıkSoyadı)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
