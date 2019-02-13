using System;
using System.Collections.Generic;
using AngolaPrev.VivaEstetica.MVC.Models.Agenda;

namespace AngolaPrev.VivaEstetica.MVC.Services.Agenda
{
    public interface IAgendaService
    {
        IEnumerable<ObterAgendamentosPorDataViewModel> GetAgendamentoPorData(DateTime data);
        void CadastrarAgendamento(CadastrarAgendamentoViewModel model);
        void CancelarAgendamento(CancelarAgendamentoViewModel model);
    }
}