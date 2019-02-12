namespace AngolaPrev.VivaEstetica.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entidades_principais : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_CLIENTES",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Cpf = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TB_SERVICOS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DS_SERVICO = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TB_SERVICOS");
            DropTable("dbo.TB_CLIENTES");
        }
    }
}
