using System.Data.Entity.ModelConfiguration;

namespace musteri_Kart_İliskisi.Entities.Mapping
{
    public class AdresTipi : EntityTypeConfiguration<Adres_Tipi>
    {
        public AdresTipi()
        {
            ToTable("Adres_Tipi");

            HasKey(at => at.AdresTipiID);

            Property(t => t.TipAdi)
                .IsRequired()
                .HasMaxLength(7);
        }
    }
}