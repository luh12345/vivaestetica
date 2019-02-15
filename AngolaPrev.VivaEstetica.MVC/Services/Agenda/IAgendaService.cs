using System;
using System.Collections.Generic;
using System.Linq;
using AngolaPrev.VivaEstetica.MVC.Models.Agenda;

namespace AngolaPrev.VivaEstetica.MVC.Services.Agenda
{
    public interface IAgendaService
    {
        IEnumerable<ObterAgendamentosPorDataViewModel> GetAgendamentoPorData(DateTime data);
        void CadastrarAgendamento(CadastrarAgendamentoViewModel model);
        void CancelarAgendamento(CancelarAgendamentoViewModel model);
        IEnumerable<string> ObterAgendamentosPendentes(int idCliente);
        IEnumerable<IGrouping<DateTime, ObterAgendamentoViewModel>> ObterHistoricoAgendamento(int idCliente);
    }
}