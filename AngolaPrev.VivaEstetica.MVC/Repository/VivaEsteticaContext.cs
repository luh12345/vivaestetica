using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using System;
using System.Collections.Generic;
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
    }
}