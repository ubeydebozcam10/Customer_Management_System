using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musteri_Kart_İliskisi.Entities
{
    public class Context : DbContext 
    {
        public Context() {
              System.Data.Entity.Database.SetInitializer<Context>(null);
        }
        public DbSet<Adres> Adres { get; set; }
        public DbSet<Adres_Tipi> Adres_Tipi { get; set; }
        public DbSet<E_mail> E_Mail { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<telefon_No> telefon_No { get; set; }
        public DbSet<telefon_Tipi> telefon_Tipi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
