namespace AngolaPrev.VivaEstetica.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qt_sessoes_agendamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_AGENDA", "QT_SESSOES_AGENDAMENTO", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_AGENDA", "QT_SESSOES_AGENDAMENTO");
        }
    }
}
