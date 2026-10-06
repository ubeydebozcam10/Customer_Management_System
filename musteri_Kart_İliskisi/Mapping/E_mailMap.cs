using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities.Mapping
{
    public class E_mailMap : EntityTypeConfiguration<E_mail>
    {
        public E_mailMap() { 
            
            HasKey(e => e.E_MailId);

            Property(a => a.EmailAdresi)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(a => a.Musteri)
                .WithMany(e => e.E_mail)   
                .HasForeignKey(a => a.MusteriId);

        }
    }
}
