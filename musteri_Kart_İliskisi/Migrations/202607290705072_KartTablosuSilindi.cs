namespace musteri_Kart_İliskisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KartTablosuSilindi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kart", "MusteriID", "dbo.Musteri");
            DropIndex("dbo.Kart", new[] { "MusteriID" });
            DropTable("dbo.Kart");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Kart",
                c => new
                    {
                        KartID = c.Int(nullable: false, identity: true),
                        KartNo = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        SonKullanmaTarihi = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        GuvenlikKodu = c.String(nullable: false, maxLength: 3, fixedLength: true),
                        KartSahibi = c.String(nullable: false, maxLength: 50),
                        Bakiye = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MusteriID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KartID);
            
            CreateIndex("dbo.Kart", "MusteriID");
            AddForeignKey("dbo.Kart", "MusteriID", "dbo.Musteri", "MusteriID", cascadeDelete: true);
        }
    }
}
