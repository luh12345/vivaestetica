using AngolaPrev.VivaEstetica.MVC.Models.Servico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

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
        public IEnumerable<SelectListItem> Servicos { get; set; }
        public string MensagemPendentes { get; set; }
        [Display(Name = "Descrição do serviço")]
        public int IdServico { get; set; }
        public int IdUsuarioLogado
        {
            get
            {
                return WebSecurity.CurrentUserId;
            }
        }
    }
}