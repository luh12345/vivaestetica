﻿using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Agenda;
using AngolaPrev.VivaEstetica.MVC.Models.Servico;
using AngolaPrev.VivaEstetica.MVC.Services.Agenda;
using AngolaPrev.VivaEstetica.MVC.Services.Serviços;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    [Authorize]
    public class AgendaController : BaseController
    {
        private readonly IAgendaService agendaService;
        private readonly IServicoService servicoService;

        public AgendaController(IAgendaService agendaService, IServicoService servicoService)
        {
            this.agendaService = agendaService;
            this.servicoService = servicoService;
        }
        // GET: Agenda
        public ActionResult Index(FiltroAgendamentoViewModel model)
        {
            if (model.DataAgendamento == null || model.DataAgendamento == default(DateTime))
                model.DataAgendamento = DateTime.Now;

            IEnumerable<ObterServicoViewModel> servicos = servicoService.ObterTodos();

            model.Servicos = servicos.Select(x => new SelectListItem { Text = x.Descricao, Value = x.IdServico.ToString() });
            model.Data = agendaService.GetAgendamentoPorData(model.DataAgendamento);

            IEnumerable<string> pendentes = agendaService.ObterAgendamentosPendentes(GetUserId());
            if (pendentes.Any())
            {
                string mensagemPendentes = $"Existem agendamentos pendentes para os seguintes serviços: {string.Join(",", pendentes)}";
                model.MensagemPendentes = mensagemPendentes;
            }

            return View(model);
        }

        public ActionResult MinhaAgenda()
        {
            IEnumerable<IGrouping<DateTime, ObterAgendamentoViewModel>> model = agendaService.ObterHistoricoAgendamento(GetUserId());
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Agendar(CadastrarAgendamentoViewModel model)
        {
            if (!ModelState.IsValid)
                Callback(new Exception("Dados inválidos"));
            else
            {
                try
                {
                    DateTime dataAgendamento = model.DataAgendamento.AddTicks(model.HorarioAgendamento.Ticks);
                    if (dataAgendamento < DateTime.Now)
                    {
                        Callback(new Exception(ExceptionMessages.DataNaoDisponivel));
                        return RedirectToAction("Index", new { model.DataAgendamento });
                    }

                    model.IdCliente = GetUserId();
                    agendaService.CadastrarAgendamento(model);
                    Callback();
                }
                catch (Exception ex)
                {
                    Callback(ex);
                }
            }

            return RedirectToAction("Index", new { model.DataAgendamento });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Cancelar(CancelarAgendamentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Callback(new Exception("Dados inválidos."));
            }
            else
            {
                try
                {
                    agendaService.CancelarAgendamento(model);
                    Callback();
                }
                catch (Exception ex)
                {
                    Callback(ex);
                }
            }

            return RedirectToAction("Index", new { model.DataAgendamento });
        }
    }
}