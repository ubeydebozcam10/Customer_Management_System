using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities.Mapping
{
    public class telefon_TipiMap : EntityTypeConfiguration<telefon_Tipi>
    {
        public telefon_TipiMap()
        {
            HasKey(t => t.TelefonTipiID);

            Property(a => a.TipAdi)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
