using AngolaPrev.VivaEstetica.MVC.Models.Servico;
using AngolaPrev.VivaEstetica.MVC.Services.Serviços;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    public class ServicosController : BaseController
    {
        private readonly IServicoService servicoService;

        public ServicosController(IServicoService servicoService)
        {
            this.servicoService = servicoService;
        }

        // GET: Servicos
        public ActionResult Index()
        {
            IEnumerable<ObterServicoViewModel> model = servicoService.GetAll();
            return View(model);
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Cadastro(CadastroServicoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                servicoService.Add(model);
                Callback();
            }
            catch (Exception ex)
            {
                Callback(ex);
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}