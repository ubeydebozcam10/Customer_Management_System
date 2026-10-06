namespace musteri_Kart_İliskisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdresDetay = c.String(nullable: false, maxLength: 50),
                        Sehir = c.String(nullable: false, maxLength: 14, fixedLength: true),
                        Ilce = c.String(nullable: false, maxLength: 50),
                        MusteriID = c.Int(nullable: false),
                        AdresTipiID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adres_Tipi", t => t.AdresTipiID)
                .ForeignKey("dbo.Musteri", t => t.MusteriID, cascadeDelete: true)
                .Index(t => t.MusteriID)
                .Index(t => t.AdresTipiID);
            
            CreateTable(
                "dbo.Adres_Tipi",
                c => new
                    {
                        AdresTipiID = c.String(nullable: false, maxLength: 128),
                        TipAdi = c.String(nullable: false, maxLength: 7),
                    })
                .PrimaryKey(t => t.AdresTipiID);
            
            CreateTable(
                "dbo.Musteri",
                c => new
                    {
                        MusteriID = c.Int(nullable: false, identity: true),
                        MusteriAdi = c.String(nullable: false),
                        MusteriSoyadi = c.String(nullable: false),
                        DogumYili = c.String(nullable: false, maxLength: 4),
                        Tc_No = c.String(nullable: false, maxLength: 11, fixedLength: true),
                        SigortaDurumu = c.String(nullable: false),
                        AnneKızlıkSoyadı = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MusteriID);
            
            CreateTable(
                "dbo.E_mail",
                c => new
                    {
                        E_MailId = c.Int(nullable: false, identity: true),
                        EmailAdresi = c.String(nullable: false, maxLength: 50),
                        MusteriId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.E_MailId)
                .ForeignKey("dbo.Musteri", t => t.MusteriId, cascadeDelete: true)
                .Index(t => t.MusteriId);
            
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
                .PrimaryKey(t => t.KartID)
                .ForeignKey("dbo.Musteri", t => t.MusteriID, cascadeDelete: true)
                .Index(t => t.MusteriID);
            
            CreateTable(
                "dbo.telefon_No",
                c => new
                    {
                        TelefonID = c.Int(nullable: false, identity: true),
                        Numara = c.String(nullable: false, maxLength: 11, fixedLength: true),
                        MusteriId = c.Int(nullable: false),
                        TelefonTipiID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TelefonID)
                .ForeignKey("dbo.Musteri", t => t.MusteriId, cascadeDelete: true)
                .ForeignKey("dbo.telefon_Tipi", t => t.TelefonTipiID, cascadeDelete: true)
                .Index(t => t.MusteriId)
                .Index(t => t.TelefonTipiID);
            
            CreateTable(
                "dbo.telefon_Tipi",
                c => new
                    {
                        TelefonTipiID = c.String(nullable: false, maxLength: 128),
                        TipAdi = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TelefonTipiID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adres", "MusteriID", "dbo.Musteri");
            DropForeignKey("dbo.telefon_No", "TelefonTipiID", "dbo.telefon_Tipi");
            DropForeignKey("dbo.telefon_No", "MusteriId", "dbo.Musteri");
            DropForeignKey("dbo.Kart", "MusteriID", "dbo.Musteri");
            DropForeignKey("dbo.E_mail", "MusteriId", "dbo.Musteri");
            DropForeignKey("dbo.Adres", "AdresTipiID", "dbo.Adres_Tipi");
            DropIndex("dbo.telefon_No", new[] { "TelefonTipiID" });
            DropIndex("dbo.telefon_No", new[] { "MusteriId" });
            DropIndex("dbo.Kart", new[] { "MusteriID" });
            DropIndex("dbo.E_mail", new[] { "MusteriId" });
            DropIndex("dbo.Adres", new[] { "AdresTipiID" });
            DropIndex("dbo.Adres", new[] { "MusteriID" });
            DropTable("dbo.telefon_Tipi");
            DropTable("dbo.telefon_No");
            DropTable("dbo.Kart");
            DropTable("dbo.E_mail");
            DropTable("dbo.Musteri");
            DropTable("dbo.Adres_Tipi");
            DropTable("dbo.Adres");
        }
    }
}
