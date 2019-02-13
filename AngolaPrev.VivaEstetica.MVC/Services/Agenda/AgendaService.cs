﻿using AngolaPrev.VivaEstetica.MVC.Common.Const;
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
                    NomePessoa = agendamento != null ? agendamento.TB_AGENDA.TB_CLIENTES.Nome : string.Empty,
                    Hora = x,
                    QuantidadeSessaoAgendamento = agendamento != null ? agendamento.TB_AGENDA.QT_SESSOES_AGENDAMENTO : 0
                };
            });
        }

        public void CadastrarAgendamento(CadastrarAgendamentoViewModel model)
        {
            TB_SERVICOS servico = context.TB_SERVICOS.Single(x => x.Id == model.IdServico);
            TB_SECOES secao = null;

            IEnumerable<TB_AGENDA> agendamentosCliente = context.TB_AGENDA
                                                .Where(x => x.ID_SERVICO == model.IdServico && x.ID_CLIENTE == model.IdCliente)
                                                .ToList();

            TB_AGENDA agendamentoMesmoServico = agendamentosCliente.Where(x => (x.DT_AGENDAMENTO - model.DataAgendamento).TotalDays == 0)
                                                    .FirstOrDefault();
            if (agendamentoMesmoServico != null)
                throw new Exception(string.Format(ExceptionMessages.ServicoJaCadastrado, model.DataAgendamento.ToShortDateString()));

            TB_AGENDA ultimoAgendamentoServico = agendamentosCliente.OrderByDescending(x => x.DT_AGENDAMENTO)
                                                    .FirstOrDefault();

            if (ultimoAgendamentoServico != null && ultimoAgendamentoServico.DT_AGENDAMENTO > model.DataAgendamento.AddTicks(model.HorarioAgendamento.Ticks))
                throw new Exception(ExceptionMessages.DataMenorQueAtual);

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
                        horarios.Add(model.HorarioAgendamento.Add(new TimeSpan(0, 30, 0)));
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
                            horarios.Add(model.HorarioAgendamento.Add(new TimeSpan(0, 30, 0)));
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
                if ((agendamento.DT_CRIACAO.AddDays(1) - DateTime.Now).Days < 0)
                    throw new Exception(ExceptionMessages.NaoEPossivelCancelarAgendamento);

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
                if ((agendamento.DT_AGENDAMENTO - DateTime.Now).Days < 1)
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
    }
}