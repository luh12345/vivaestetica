namespace AngolaPrev.VivaEstetica.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RL_SECAO_AGENDAMENTO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ID_AGENDAMENTO = c.Int(nullable: false),
                        ID_SECAO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_AGENDA", t => t.ID_AGENDAMENTO, cascadeDelete: true)
                .ForeignKey("dbo.TB_SECOES", t => t.ID_SECAO, cascadeDelete: true)
                .Index(t => t.ID_AGENDAMENTO)
                .Index(t => t.ID_SECAO);
            
            CreateTable(
                "dbo.TB_AGENDA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DT_AGENDAMENTO = c.DateTime(nullable: false),
                        ID_CLIENTE = c.Int(nullable: false),
                        ID_SERVICO = c.Int(nullable: false),
                        DT_CRIACAO = c.DateTime(nullable: false),
                        QT_SESSOES_AGENDAMENTO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_CLIENTES", t => t.ID_CLIENTE, cascadeDelete: true)
                .ForeignKey("dbo.TB_SERVICOS", t => t.ID_SERVICO, cascadeDelete: true)
                .Index(t => t.ID_CLIENTE)
                .Index(t => t.ID_SERVICO);
            
            CreateTable(
                "dbo.TB_CLIENTES",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DS_NOME = c.String(),
                        DS_EMAIL = c.String(),
                        CPF = c.String(),
                        DS_ENDERECO = c.String(),
                        NU_TELEFONE = c.String(),
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
                        BT_MASSAGEM = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TB_SECOES",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DS_IDENTIFICADOR = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RL_SECAO_AGENDAMENTO", "ID_SECAO", "dbo.TB_SECOES");
            DropForeignKey("dbo.RL_SECAO_AGENDAMENTO", "ID_AGENDAMENTO", "dbo.TB_AGENDA");
            DropForeignKey("dbo.TB_AGENDA", "ID_SERVICO", "dbo.TB_SERVICOS");
            DropForeignKey("dbo.TB_AGENDA", "ID_CLIENTE", "dbo.TB_CLIENTES");
            DropIndex("dbo.TB_AGENDA", new[] { "ID_SERVICO" });
            DropIndex("dbo.TB_AGENDA", new[] { "ID_CLIENTE" });
            DropIndex("dbo.RL_SECAO_AGENDAMENTO", new[] { "ID_SECAO" });
            DropIndex("dbo.RL_SECAO_AGENDAMENTO", new[] { "ID_AGENDAMENTO" });
            DropTable("dbo.TB_SECOES");
            DropTable("dbo.TB_SERVICOS");
            DropTable("dbo.TB_CLIENTES");
            DropTable("dbo.TB_AGENDA");
            DropTable("dbo.RL_SECAO_AGENDAMENTO");
        }
    }
}
