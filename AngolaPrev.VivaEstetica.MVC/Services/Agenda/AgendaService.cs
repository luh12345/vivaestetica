using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Agenda;
using AngolaPrev.VivaEstetica.MVC.Repository;
using AngolaPrev.VivaEstetica.MVC.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            IEnumerable<RL_SECAO_AGENDAMENTO> agendamentos = context.RL_SECAO_AGENDAMENTO
                .Include(x => x.TB_AGENDA)
                .Include(x => x.TB_SECOES)
                .Include(x => x.TB_AGENDA.TB_CLIENTES)
                .Include(x => x.TB_AGENDA.TB_SERVICOS)
                .Where(x => DbFunctions.DiffDays(x.TB_AGENDA.DT_AGENDAMENTO, data) == 0).ToArray();

            return HorariosAtendimento.Horarios.Select(x =>
            {
                RL_SECAO_AGENDAMENTO agendamento = agendamentos.Where(y => y.TB_AGENDA.DT_AGENDAMENTO.TimeOfDay.TotalMinutes == x.TotalMinutes).FirstOrDefault();
                return new ObterAgendamentosPorDataViewModel
                {
                    IdentificadorSecao = agendamento != null ? agendamento.TB_SECOES.DS_IDENTIFICADOR : string.Empty,
                    IdAgendamento = agendamento != null ? agendamento.Id : 0,
                    DataAgendamento = agendamento != null ? agendamento.TB_AGENDA.DT_AGENDAMENTO : default(DateTime),
                    DescricaoServico = agendamento != null ? agendamento.TB_AGENDA.TB_SERVICOS.DS_SERVICO : string.Empty,
                    NomePessoa = agendamento != null ? agendamento.TB_AGENDA.TB_CLIENTES.DS_NOME : string.Empty,
                    IdCliente = agendamento != null ? agendamento.TB_AGENDA.ID_CLIENTE : 0,
                    IdServico = agendamento != null ? agendamento.TB_AGENDA.ID_SERVICO : 0,
                    Hora = x,
                    QuantidadeSessaoAgendamento = agendamento != null ? agendamento.TB_AGENDA.QT_SESSOES_AGENDAMENTO : 0
                };
            });
        }

        public void CadastrarAgendamento(CadastrarAgendamentoViewModel model)
        {
            TB_SERVICOS servico = context.TB_SERVICOS.Single(x => x.Id == model.IdServico);
            TB_SECOES secao = null;

            if (model.QuantidadeSessao > 0 && model.QuantidadeSessao <= servico.QT_SESSOES)
            {
                List<TimeSpan> horarios = new List<TimeSpan>
                {
                    model.HorarioAgendamento
                };

                if (model.QuantidadeSessao == 1 && !servico.BT_MASSAGEM)
                {
                    TimeSpan horasServico = TimeSpan.FromMinutes(servico.TP_MINUTOS);

                    if (horasServico.Hours == 1)
                    {
                        TimeSpan maisTrintaMinutos = model.HorarioAgendamento.Add(new TimeSpan(0, 30, 0));
                        DateTime proximosTrintaMinutos = model.DataAgendamento.AddTicks(maisTrintaMinutos.Ticks);

                        TB_AGENDA proximoHorarioOcupado = context.TB_AGENDA.SingleOrDefault(x => x.DT_AGENDAMENTO == proximosTrintaMinutos);
                        if (proximoHorarioOcupado != null)
                            throw new Exception(string.Format(ExceptionMessages.ProximoHorarioInvalido, proximosTrintaMinutos.ToLongTimeString(), proximosTrintaMinutos.ToLongDateString()));

                        horarios.Add(maisTrintaMinutos);
                    }

                    secao = new TB_SECOES
                    {
                        DS_IDENTIFICADOR = Guid.NewGuid().ToString()
                    };
                    context.TB_SECOES.Add(secao);
                    context.SaveChanges();
                }
                else
                {
                    int[] agendamentos = context.TB_AGENDA
                                                          .Include(x => x.TB_CLIENTES)
                                                          .Include(x => x.TB_SERVICOS)
                                                          .Where(x => x.ID_CLIENTE == model.IdCliente && x.ID_SERVICO == model.IdServico)
                                                          .Select(x => x.Id)
                                                          .ToArray();

                    IEnumerable<IGrouping<string, RL_SECAO_AGENDAMENTO>> sessoes = context.RL_SECAO_AGENDAMENTO
                                                                .Include(x => x.TB_SECOES)
                                                                .Where(x => agendamentos.Contains(x.TB_AGENDA.Id))
                                                                .ToArray()
                                                                .GroupBy(x => x.TB_SECOES.DS_IDENTIFICADOR);

                    IEnumerable<RL_SECAO_AGENDAMENTO> rl_secao_agendamento = sessoes.Where(x => x.Count() < servico.QT_SESSOES)
                                                         .FirstOrDefault();

                    if (rl_secao_agendamento != null && rl_secao_agendamento.Any())
                    {
                        secao = rl_secao_agendamento.FirstOrDefault().TB_SECOES;
                    }

                    if (secao == null)
                    {
                        secao = new TB_SECOES
                        {
                            DS_IDENTIFICADOR = Guid.NewGuid().ToString()
                        };

                        context.TB_SECOES.Add(secao);
                        context.SaveChanges();

                        TimeSpan horasServico = TimeSpan.FromMinutes(servico.TP_MINUTOS);
                        if (horasServico.Hours == 1 && model.QuantidadeSessao == 1)
                        {
                            TimeSpan maisTrintaMinutos = model.HorarioAgendamento.Add(new TimeSpan(0, 30, 0));
                            DateTime proximosTrintaMinutos = model.DataAgendamento.AddTicks(maisTrintaMinutos.Ticks);

                            TB_AGENDA proximoHorarioOcupado = context.TB_AGENDA.SingleOrDefault(x => x.DT_AGENDAMENTO == proximosTrintaMinutos);
                            if (proximoHorarioOcupado != null)
                                throw new Exception(string.Format(ExceptionMessages.ProximoHorarioInvalido, proximosTrintaMinutos.ToLongTimeString(), proximosTrintaMinutos.ToLongDateString()));

                            horarios.Add(maisTrintaMinutos);
                        }
                    }
                    else
                    {
                        model.QuantidadeSessao = servico.QT_SESSOES;
                    }
                }

                foreach (TimeSpan horario in horarios)
                {
                    DateTime dataAgendamento = model.DataAgendamento.AddTicks(horario.Ticks);
                    TB_AGENDA agendamento = new TB_AGENDA
                    {
                        DT_AGENDAMENTO = dataAgendamento,
                        DT_CRIACAO = DateTime.Now,
                        ID_CLIENTE = model.IdCliente,
                        ID_SERVICO = model.IdServico,
                        QT_SESSOES_AGENDAMENTO = model.QuantidadeSessao
                    };

                    context.TB_AGENDA.Add(agendamento);

                    RL_SECAO_AGENDAMENTO secaoAgendamento = new RL_SECAO_AGENDAMENTO
                    {
                        ID_AGENDAMENTO = agendamento.Id,
                        ID_SECAO = secao.Id
                    };

                    context.RL_SECAO_AGENDAMENTO.Add(secaoAgendamento);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new Exception(string.Format(ExceptionMessages.QuantidadeDeSessoesNaoPermitida, servico.QT_SESSOES));
            }
        }

        public void CancelarAgendamento(CancelarAgendamentoViewModel model)
        {
            TB_AGENDA agendamento = context.TB_AGENDA
                                     .Include(x => x.TB_SERVICOS)
                                     .Single(x => x.Id == model.IdAgendamento);

            //regra de cancelamento para massagens 
            if (agendamento.TB_SERVICOS.BT_MASSAGEM)
            {
                if ((agendamento.DT_AGENDAMENTO - DateTime.Now).TotalHours < 24)
                    throw new Exception(ExceptionMessages.CancelarVinteQuatroHorasAntecedencia);

                if (agendamento.QT_SESSOES_AGENDAMENTO == 1)
                {
                    RemoverSessoes(model.IdAgendamento);
                }
                else
                {
                    RL_SECAO_AGENDAMENTO sessaoAgendamento = context.RL_SECAO_AGENDAMENTO
                                                              .Include(x => x.TB_SECOES)
                                                              .Single(x => x.ID_AGENDAMENTO == model.IdAgendamento);
                    context.TB_AGENDA.Remove(agendamento);
                    context.RL_SECAO_AGENDAMENTO.Remove(sessaoAgendamento);

                    context.SaveChanges();
                }
            }
            else //regra de cancelamento para demais serviços
            {
                if (DateTime.Now > agendamento.DT_AGENDAMENTO.Date)
                    throw new Exception(ExceptionMessages.NaoEPossivelCancelarAgendamento);

                RemoverSessoes(model.IdAgendamento);
            }
        }

        private void RemoverSessoes(int idAgendamento)
        {
            int idSecao = context.RL_SECAO_AGENDAMENTO.Single(x => x.ID_AGENDAMENTO == idAgendamento).ID_SECAO;
            IEnumerable<RL_SECAO_AGENDAMENTO> secaoAgendamentos = context.RL_SECAO_AGENDAMENTO
                                                                    .Include(x => x.TB_AGENDA)
                                                                    .Include(x => x.TB_SECOES)
                                                                    .Where(x => x.ID_SECAO == idSecao).ToArray();

            IEnumerable<TB_AGENDA> agendamentos = secaoAgendamentos.Select(x => x.TB_AGENDA).ToArray();
            IEnumerable<TB_SECOES> secoes = secaoAgendamentos.Select(x => x.TB_SECOES).ToArray();

            context.TB_AGENDA.RemoveRange(agendamentos);
            context.TB_SECOES.RemoveRange(secoes);
            context.RL_SECAO_AGENDAMENTO.RemoveRange(secaoAgendamentos);
            context.SaveChanges();
        }

        public IEnumerable<string> ObterAgendamentosPendentes(int idCliente)
        {
            IEnumerable<IGrouping<string, RL_SECAO_AGENDAMENTO>> agendamentos = context.RL_SECAO_AGENDAMENTO
                                                             .Include(x => x.TB_AGENDA)
                                                             .Include(x => x.TB_SECOES)
                                                             .Include(x => x.TB_AGENDA.TB_SERVICOS)
                                                             .Where(x => x.TB_AGENDA.ID_CLIENTE == idCliente && x.TB_AGENDA.QT_SESSOES_AGENDAMENTO > 1)
                                                             .ToArray()
                                                             .GroupBy(x => x.TB_SECOES.DS_IDENTIFICADOR);

            IEnumerable<string> pendentes = agendamentos.Where(x => x.Any(y => x.Count() < y.TB_AGENDA.QT_SESSOES_AGENDAMENTO))
                                                          .Select(x => x.First().TB_AGENDA.TB_SERVICOS.DS_SERVICO);

            return pendentes;
        }

        public IEnumerable<IGrouping<DateTime, ObterAgendamentoViewModel>> ObterHistoricoAgendamento(int idCliente)
        {
            return context.TB_AGENDA.Include(x => x.TB_SERVICOS)
                                    .Where(x => x.ID_CLIENTE == idCliente)
                                    .ToArray()
                                    .Select(x => new ObterAgendamentoViewModel
                                    {
                                        HoraAgendamento = x.DT_AGENDAMENTO.TimeOfDay,
                                        DataAgendamento = x.DT_AGENDAMENTO,
                                        NomeServico = x.TB_SERVICOS.DS_SERVICO
                                    })
                                    .OrderByDescending(x => x.DataAgendamento)
                                    .GroupBy(x => x.DataAgendamento.Date);
        }
    }
}