namespace AngolaPrev.VivaEstetica.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class att_tbservicos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_SERVICOS", "QT_SESSOES", c => c.Int(nullable: false));
            AddColumn("dbo.TB_SERVICOS", "TP_MINUTOS", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_SERVICOS", "TP_MINUTOS");
            DropColumn("dbo.TB_SERVICOS", "QT_SESSOES");
        }
    }
}
