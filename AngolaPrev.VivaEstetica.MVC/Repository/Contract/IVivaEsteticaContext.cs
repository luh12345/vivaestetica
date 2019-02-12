using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngolaPrev.VivaEstetica.MVC.Repository.Contract
{
    public interface IVivaEsteticaContext
    {
        DbSet<TB_CLIENTES> TB_CLIENTES { get; set; }
        DbSet<TB_SERVICOS> TB_SERVICOS { get; set; }
        DbSet<TB_AGENDA> TB_AGENDA { get; set; }

        int SaveChanges();
    }
}
