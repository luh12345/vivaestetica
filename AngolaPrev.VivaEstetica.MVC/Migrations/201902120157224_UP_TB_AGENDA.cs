namespace AngolaPrev.VivaEstetica.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UP_TB_AGENDA : DbMigration
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
                    QT_SESSOES = c.Int(nullable: false),
                    TP_MINUTOS = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
            "dbo.TB_AGENDA",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                DT_AGENDAMENTO = c.DateTime(nullable: false),
                ID_CLIENTE = c.Int(nullable: false),
                ID_SERVICO = c.Int(nullable: false),
            })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.TB_CLIENTES", t => t.ID_CLIENTE, cascadeDelete: true)
            .ForeignKey("dbo.TB_SERVICOS", t => t.ID_SERVICO, cascadeDelete: true)
            .Index(t => t.ID_CLIENTE)
            .Index(t => t.ID_SERVICO);

        }

        public override void Down()
        {
            DropForeignKey("dbo.TB_AGENDA", "ID_SERVICO", "dbo.TB_SERVICOS");
            DropForeignKey("dbo.TB_AGENDA", "ID_CLIENTE", "dbo.TB_CLIENTES");
            DropIndex("dbo.TB_AGENDA", new[] { "ID_SERVICO" });
            DropIndex("dbo.TB_AGENDA", new[] { "ID_CLIENTE" });
            DropTable("dbo.TB_SERVICOS");
            DropTable("dbo.TB_CLIENTES");
            DropTable("dbo.TB_AGENDA");
        }
    }
}
