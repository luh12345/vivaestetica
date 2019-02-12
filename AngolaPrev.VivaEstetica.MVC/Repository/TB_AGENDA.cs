using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class TB_AGENDA
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TB_SERVICOS")]
        public int IdServico { get; set; }
        public TB_SERVICOS TB_SERVICOS { get; set; }
        [ForeignKey("TB_CLIENTES")]
        public int IdCliente { get; set; }
        public TB_CLIENTES TB_CLIENTES { get; set; }
        public DateTime DT_AGENDAMENTO { get; set; }
    }
}