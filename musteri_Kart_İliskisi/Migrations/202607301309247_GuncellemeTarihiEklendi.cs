namespace musteri_Kart_İliskisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuncellemeTarihiEklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musteri", "GuncellemeTarihi", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musteri", "GuncellemeTarihi");
        }
    }
}
