using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Agenda
{
    public class ObterAgendamentoViewModel
    {
        public TimeSpan HoraAgendamento { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string NomeServico { get; set; }
    }
}