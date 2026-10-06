namespace musteri_Kart_İliskisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KayıtTarihiEklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musteri", "KayıtTarihi", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musteri", "KayıtTarihi");
        }
    }
}
