using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Models.Agenda
{
    public class CadastrarAgendamentoViewModel
    {
        public int IdCliente { get; set; }
        public int IdServico { get; set; }
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
        public TimeSpan HorarioAgendamento { get; set; }
        public int QuantidadeSessao { get; set; }
    }
}