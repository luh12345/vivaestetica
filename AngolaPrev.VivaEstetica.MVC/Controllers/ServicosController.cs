using AngolaPrev.VivaEstetica.MVC.Common.Const;
using AngolaPrev.VivaEstetica.MVC.Models.Servico;
using AngolaPrev.VivaEstetica.MVC.Services.Serviços;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngolaPrev.VivaEstetica.MVC.Controllers
{
    [Authorize]
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
            IEnumerable<ObterServicoViewModel> model = servicoService.ObterTodos();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Cadastro(CadastroServicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Callback(new Exception(ExceptionMessages.DadosInvalidos));
            }
            else
            {
                try
                {
                    servicoService.CadastrarServico(model);
                    Callback();
                }
                catch (Exception ex)
                {
                    Callback(ex);
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Editar(ObterServicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Callback(new Exception(ExceptionMessages.DadosInvalidos));
            }
            else
            {
                try
                {
                    servicoService.EditarServico(model);
                    Callback();
                }
                catch (Exception ex)
                {
                    Callback(ex);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Deletar(int IdServico)
        {
            if (IdServico <= 0)
            {
                Callback(new Exception(ExceptionMessages.DadosInvalidos));
            }
            else
            {
                servicoService.DeletarServico(IdServico);
                Callback();
            }
            return RedirectToAction("Index");
        }
    }
}