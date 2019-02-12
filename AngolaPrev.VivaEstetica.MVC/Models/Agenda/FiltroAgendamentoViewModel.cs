using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Agenda
{
    public class FiltroAgendamentoViewModel
    {
        public DateTime DataAgendamento { get; set; }
        public string DataAgendamentoFormatted
        {
            get
            {
                return this.DataAgendamento.ToShortDateString();
            }
            set
            {
                if (DateTime.TryParse(value, out DateTime result))
                {
                    DataAgendamento = result;
                }
            }
        }
        public IEnumerable<ObterAgendamentosPorDataViewModel> Data { get; set; }
    }
}