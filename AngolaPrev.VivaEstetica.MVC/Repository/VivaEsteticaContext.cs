using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class VivaEsteticaContext : DbContext, IVivaEsteticaContext
    {
        public DbSet<TB_CLIENTES> TB_CLIENTES { get; set; }
        public DbSet<TB_SERVICOS> TB_SERVICOS { get; set; }
        public DbSet<TB_AGENDA> TB_AGENDA { get; set; }
        public DbSet<TB_SECOES> TB_SECOES { get; set; }
        public DbSet<RL_SECAO_AGENDAMENTO> RL_SECAO_AGENDAMENTO { get; set; }

        public VivaEsteticaContext() : base(ConfigurationManager.AppSettings["context.name"])
        {

        }

    }
}