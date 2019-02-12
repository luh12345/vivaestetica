using AngolaPrev.VivaEstetica.MVC.Models.Agenda;
using AngolaPrev.VivaEstetica.MVC.Services.Agenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendaService agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            this.agendaService = agendaService;
        }
        // GET: Agenda
        public ActionResult Index(FiltroAgendamentoViewModel model)
        {
            if (model.DataAgendamento == null || model.DataAgendamento == default(DateTime))
                model.DataAgendamento = DateTime.Now;

            model.Data = agendaService.GetAgendamentoPorData(model.DataAgendamento);
            return View(model);
        }
    }
}