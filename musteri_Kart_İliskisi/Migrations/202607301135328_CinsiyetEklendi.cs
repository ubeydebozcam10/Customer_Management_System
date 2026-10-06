namespace musteri_Kart_İliskisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CinsiyetEklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musteri", "Cinsiyet", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musteri", "Cinsiyet");
        }
    }
}
