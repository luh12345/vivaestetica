using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Repository
{
    public class RL_SECAO_AGENDAMENTO
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TB_AGENDA")]
        public int ID_AGENDAMENTO { get; set; }
        public TB_AGENDA TB_AGENDA { get; set; }
        [ForeignKey("TB_SECOES")]
        public int ID_SECAO { get; set; }
        public TB_SECOES TB_SECOES { get; set; }
    }
}