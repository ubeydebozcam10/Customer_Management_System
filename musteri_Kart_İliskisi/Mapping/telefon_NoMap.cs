using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities.Mapping
{
    public class telefon_NoMap : EntityTypeConfiguration<telefon_No>
    {
        public telefon_NoMap() { 
            
            HasKey(t => t.TelefonID);

            Property(n => n.Numara)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength();

            HasRequired(a => a.Musteri)
               .WithMany(m => m.Telefon)   
               .HasForeignKey(a => a.MusteriId);

            HasRequired(tp => tp.TelefonTipi)         
                .WithMany()                          
                .HasForeignKey(t => t.TelefonTipiID);
        }
    }
}
