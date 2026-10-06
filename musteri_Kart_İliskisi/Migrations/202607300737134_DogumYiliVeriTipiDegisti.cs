namespace musteri_Kart_İliskisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogumYiliVeriTipiDegisti : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musteri", "DogumTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Musteri", "DogumYili");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Musteri", "DogumYili", c => c.String(nullable: false, maxLength: 4));
            DropColumn("dbo.Musteri", "DogumTarihi");
        }
    }
}
