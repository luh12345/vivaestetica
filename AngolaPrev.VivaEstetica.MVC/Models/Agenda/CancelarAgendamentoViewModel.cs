using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Agenda
{
    public class CancelarAgendamentoViewModel
    {
        public int IdAgendamento { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string DataAgendamentoFormatted
        {
            get
            {
                return DataAgendamento.ToShortDateString();
            }
            set
            {
                if (DateTime.TryParse(value, out DateTime result))
                {
                    DataAgendamento = result;
                }
            }
        }
    }
}