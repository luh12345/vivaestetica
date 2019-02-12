using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Agenda;
using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AngolaPrev.VivaEstetica.MVC.Services.Agenda
{
    public class AgendaService : IAgendaService
    {
        private readonly IVivaEsteticaContext context;

        public AgendaService(IVivaEsteticaContext context)
        {
            this.context = context;
        }

        public IEnumerable<ObterAgendamentosPorDataViewModel> GetAgendamentoPorData(DateTime data)
        {
            IEnumerable<TB_AGENDA> agendamentos = context.TB_AGENDA
                .Include(x => x.TB_CLIENTES)
                .Include(x => x.TB_SERVICOS)
                .Where(x => DbFunctions.DiffDays(x.DT_AGENDAMENTO, data) == 1).ToArray();

            return HorariosAtendimento.Horarios.Select(x =>
            {
                TB_AGENDA agendamento = agendamentos.Where(y => y.DT_AGENDAMENTO.TimeOfDay.TotalMinutes == x.TotalMinutes).FirstOrDefault();
                return new ObterAgendamentosPorDataViewModel
                {
                    IdAgendamento = agendamento != null ? agendamento.Id : 0,
                    DataAgendamento = agendamento != null ? agendamento.DT_AGENDAMENTO : default(DateTime),
                    DescricaoServico = agendamento != null ? agendamento.TB_SERVICOS.DS_SERVICO : string.Empty,
                    NomePessoa = agendamento != null ? agendamento.TB_CLIENTES.Nome : string.Empty,
                    Hora = x
                };
            });
        }
    }
}