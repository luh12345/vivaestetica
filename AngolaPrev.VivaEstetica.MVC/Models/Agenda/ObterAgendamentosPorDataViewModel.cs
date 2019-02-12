using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Agenda
{
    public class ObterAgendamentosPorDataViewModel
    {
        public int IdAgendamento { get; set; }
        public string DescricaoServico { get; set; }
        public string NomePessoa { get; set; }
        public DateTime DataAgendamento { get; set; }
        public TimeSpan Hora { get; set; }
    }
}