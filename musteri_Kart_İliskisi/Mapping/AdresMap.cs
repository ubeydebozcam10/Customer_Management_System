using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities.Mapping
{
    public class AdresMap : EntityTypeConfiguration<Adres>
    {
        public AdresMap()
        {
            ToTable("Adres");

            HasKey(a => a.Id);

            Property(ad => ad.AdresDetay)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.Sehir)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength();

            Property(i => i.Ilce)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.AdresTipiID)
                .HasColumnName("AdresTipiID");

            HasRequired(a => a.Musteri)
                .WithMany(m => m.Adres)
                .HasForeignKey(a => a.MusteriID);

            HasRequired(a => a.AdresTipi)
            .WithMany()
            .HasForeignKey(a => a.AdresTipiID)
            .WillCascadeOnDelete(false);
        }
    }

}

