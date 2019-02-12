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
        public DateTime DT_AGENDAMENTO { get; set; }
        [ForeignKey("TB_CLIENTES")]
        public int ID_CLIENTE { get; set; }
        public TB_CLIENTES TB_CLIENTES { get; set; }
        [ForeignKey("TB_SERVICOS")]
        public int ID_SERVICO { get; set; }
        public TB_SERVICOS TB_SERVICOS { get; set; }
    }
}